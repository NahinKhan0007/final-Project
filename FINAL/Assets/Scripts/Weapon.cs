using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bullet; //Reference to bullet spawned
    public Transform shotPoint; //Position of bullet spawned
    public float timeBetweenShots; //How much time has to pass before shooting next bullet (frequency)

    private float shotTime; //Time to shoot another bullet

    // Update is called once per frame
    void Update()
    {

        //Weapon points in the direction of the mouse (mousePos - transformPos)
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        //Angle radians to degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //Converting angle into quaternion
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = rotation;

        //Left mouse button pressed
        if (Input.GetMouseButton(0))
        {
            //If we allowed to shoot
            if (Time.time >= shotTime)
            {
                //Spawn Projection, Position and angle of bullet
                Instantiate(bullet, shotPoint.position, transform.rotation);
                shotTime = Time.time + timeBetweenShots;
            }
        }
    }
}
