using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //Public variables, editable from inspector
    public float speed;
    public int health;

    public Image[] hearts; //Health UI
    public Sprite fullHeart; //Red hearts   
    public Sprite emptyHeart; //Blue hearts

    //Private variables, not editable in unity inspector
    private Rigidbody2D rb; //Physics in Unity by default
    private Animator anim; //Reference to animator

    private Vector2 moveAmount; //How much we want to move by

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //Player controls, controlling horizontal and vertical movement using keys/WASD
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        //using normalised so player doesn't move faster diagonally
        moveAmount = moveInput.normalized * speed; 

        //Checking if player is moving, "if he isn't still"
        if (moveInput != Vector2.zero)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
    }

    //Called every single frame
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveAmount * Time.fixedDeltaTime);

    }


    //Player taking damage, same as Enemy
    public void Damage(int damageAmount)
    {
        //Enemy Health - Damage taken
        health -= damageAmount;

        //Updating health UI / Hearts
        UpdateHealthUI(health);

        //Checks is health is lower than 0, then destroy
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void ChangeWeapon(Weapon weaponToEquip)
    {
        //Destroy weapon player is holding
        Destroy(GameObject.FindGameObjectWithTag("Weapon"));
        //Equip new weapon
        Instantiate(weaponToEquip, transform.position, transform.rotation, transform);
    }


    //Health UI
    void UpdateHealthUI(int currentHealth)
    {
        //Heart Array, i = num. of hearts
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                //Heart sprite to full/red
                hearts[i].sprite = fullHeart;
            } else
            {
                //Turn heart sprite to blue
                hearts[i].sprite = emptyHeart;
            }
        }
    }

    //Healing player through pickups
    public void Heal(int healAmount)
    {
        //Checks if player already has full health, health cap of 5
        if (health + healAmount > 5)
        {
            health = 5;
        } else
        {
            //If player hasn't got 5 health. Heal.
            health += healAmount;
        }
        UpdateHealthUI(health);
    }

}
