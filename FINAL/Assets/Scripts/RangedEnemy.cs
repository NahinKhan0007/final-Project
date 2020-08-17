using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    public float stopDistance; //range before stopping
    public Transform shotPoint; //Location where bullet will come out from
    public GameObject enemyBullet; //Enemy bullet

    private float attackTime; //Controls frequency of attack
    private Animator anim; //Accessing animator functions

    public override void Start()
    {
        base.Start(); //Calling code inside "Enemy" class
        anim = GetComponent<Animator>();
    }


    private void Update()
    {
        //Check if player is dead
        if (player != null)
        {
            //Move towards player if player too far
            if (Vector2.Distance(transform.position, player.position) > stopDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }

            if (Time.time >= attackTime)
            {
                //Sending out bullet when mouth is open, attack frequency
                attackTime = Time.time + timeBetweenAttacks;
                anim.SetTrigger("attack");
            }
        }
    }

    public void RangedAttack()
    {
        //Code from "Weapons" script, making sure bullet is the right way
        Vector2 direction = player.position - shotPoint.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        shotPoint.rotation = rotation;

        Instantiate(enemyBullet, shotPoint.position, shotPoint.rotation); //Instantiating buller at right rotation and position
    }

}
