using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;
    public float speed;

    //Used to restrain the camera and not go out of bounds
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    private void Start()
    {
        //Camera position = Player position at the start
        transform.position = playerTransform.position;
    }

    private void Update()
    {
        //Checking if player is dead
        if (playerTransform != null)
        {
            //Restraining the camera
            float clampedX = Mathf.Clamp(playerTransform.position.x, minX, maxX);
            float clampedY = Mathf.Clamp(playerTransform.position.y, minY, maxY);

            //Lerping to player position so camera movement is smooth
            transform.position = Vector2.Lerp(transform.position, new Vector2(clampedX, clampedY), speed);
        }
    }
}
