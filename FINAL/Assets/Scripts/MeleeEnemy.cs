using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    public float stopDistance; //Distance after which enemy stops
    public float attackSpeed; //Speed of the leap

    private float attackTime;

    private void Update()
    {
        //Checking if player is dead
        if(player != null)
        {
            //If enemy is too far away from player
            if (Vector2.Distance(transform.position, player.position) > stopDistance)
            {
                //Continue moving towards player
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            } else
            {
                //Enemy is close enough to attack
                if(Time.time >= attackTime)
                {
                    //attack
                    StartCoroutine(Attack());

                    //Set timer back 
                    attackTime = Time.time + timeBetweenAttacks;
                }
            }

        }
    }


    //Melee Attack
    IEnumerator Attack()
    {
        player.GetComponent<Player>().Damage(damage);

        //position of melee Enemy before leaping towards player
        Vector2 originalPosition = transform.position;
        //Target position
        Vector2 targetPosition = player.position;

        //Stores percentage amount of animation completed
        float percent = 0;

        //While animation isn't finished
        while (percent <= 1)
        {
            //Increment the animation every frame
            percent += Time.deltaTime * attackSpeed;
            float formula = (-Mathf.Pow(percent, 2) + percent) * 4;
            //Move the enemy transform according to path
            transform.position = Vector2.Lerp(originalPosition, targetPosition, formula);
            yield return null;
        }

    }

}
