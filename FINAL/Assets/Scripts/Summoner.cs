using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoner : Enemy
{
    public float minX; //Ranges
    public float maxX;
    public float minY;
    public float maxY;

    public float timeBetweenSummons; //Frequency of summoning
    private float summonTime; //Exact time of summoning

    public Enemy enemyToSummon;
    public float attackSpeed; //Attack speed
    public float stopDistance;

    private Vector2 targetPosition; //Random spot on the map where he'll summon minions
    private Animator anim; //reference to animator
    private float timer;


    public override void Start()
    {
        base.Start(); //Calls start function in enemy script
        float randomX = Random.Range(minX, maxX); //Finds random spots around the map
        float randomY = Random.Range(minY, maxY);
        targetPosition = new Vector2(randomX, randomY);
        anim = GetComponent<Animator>(); //reference to animator

    }

    private void Update()
    {
        //Checks if player is alive
        if (player != null)
        {
            //If player is too far away
            if(Vector2.Distance(transform.position, targetPosition) > 5f)
            {
                //Move towards player
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                anim.SetBool("isRunning", true);
            }  else
            {
                //Summoning spot reached
                anim.SetBool("isRunning", false);

                if (Time.time >= summonTime)
                {
                    summonTime = Time.time + timeBetweenSummons;
                    anim.SetTrigger("summon");
                }
            }

            //If player is close enough
            if (Vector2.Distance(transform.position, player.position) < stopDistance)
            {
                //Attack player (melee attack)
                if (Time.time > timer)
                {
                    timer = Time.time + timeBetweenAttacks;
                    StartCoroutine(Attack());
                }

            }

        }
    }

    public void Summon()
    {
        //Player alive check
        if(player != null)
        {
            //Summon minion
            Instantiate(enemyToSummon, transform.position, transform.rotation);
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
            transform.position = Vector2.Lerp(originalPosition, targetPosition, formula);
            yield return null;
        }

    }

}
