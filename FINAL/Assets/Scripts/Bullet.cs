using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed; //Speed of bullet
    public float lifeTime; //How long bullet stays on screen

    public GameObject explosion; //Particle effect

    public int damage; //Damage dealt to enemies

    private void Start()
    {
        //Destroy  bullet
        Invoke("DestroyBullet", lifeTime);
    }

    private void Update()
    {
        //Propelling the bullet forward
        transform.Translate(Vector2.up * speed * Time.deltaTime); 
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
        //Spawning explosion, at a position with certain rotation
        Instantiate(explosion, transform.position, Quaternion.identity);
        
    }

    //Deticting collisions
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If bullet collides with object with enemy tag, then deal damage then destroy
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().Damage(damage);
            DestroyBullet();
        }
    }

}
