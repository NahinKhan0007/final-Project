using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Weapon weaponToEquip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If Player collides with pickup
        if (collision.tag == "Player")
        {
            //Swapping weapon
            collision.GetComponent<Player>().ChangeWeapon(weaponToEquip);
            //Destroying pickup
            Destroy(gameObject);
        }
    }
}
