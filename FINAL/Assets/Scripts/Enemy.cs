using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;

    [HideInInspector]
    public Transform player;
    public float speed;
    public float timeBetweenAttacks; //So that enemies can't attack every second
    public int damage; //Damage enemies deal

    public int pickupChance; //chance of picking up weapon %
    public GameObject[] pickups; //Array contains all pickups

    public GameObject deathEffect; //Particle effect when enemy dies

    public virtual void Start()
    {
        //player is the gameObject with the tag "Player"
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    //Enemies taking damage
    public void Damage(int damageAmount)
    {
        //Enemy Health - Damage taken
        health -= damageAmount;
        //Checks is health is lower than 0, then destroy
        if(health <= 0)
        {
            //If enemy is dead
            int randomNumber = Random.Range(0, 101);
            //If randomNumber is smaller than pickupChance then spawn weapon
            if (randomNumber < pickupChance)
            {
                GameObject randomPickup = pickups[Random.Range(0, pickups.Length)];
                Instantiate(randomPickup, transform.position, transform.rotation);
            }
            //Particle effect
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }


}
