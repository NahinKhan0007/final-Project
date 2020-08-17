using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    Player playerScript;
    public int healAmount; //Amount that player will heal by

    private void Start()
    {
        //Recognising player
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>(); 
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //Heal
            playerScript.Heal(healAmount);
            //Destroy pickup game object
            Destroy(gameObject);
        }
    }
}
