using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private Player playerScript; //Variable to deal damage to player
    private Vector2 targetPosition;

    public float speed;
    public int damage;

    public GameObject effect;

    private void Start()
    {
        //Player
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        targetPosition = playerScript.transform.position;

    }

    private void Update()
    {
        //If player is not close enough move towards player
        if(Vector2.Distance(transform.position, targetPosition) > .1f)
        {
            //Particle effect
            Instantiate(effect, transform.position, Quaternion.identity);
            //Keep moving
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        } else
        {
            //If bullet hits, bullet is destroyed
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //Player is damaged
            playerScript.Damage(damage);
            Destroy(gameObject);
        }
    }
}
