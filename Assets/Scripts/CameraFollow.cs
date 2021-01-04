using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform playerTransform;

    public float offsetX;

    public float minX;
    public float maxX;

    public float minY = -2.5f;
    public float maxY = 3f;

    public float offsetY = 4.5f;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        Vector2 tmp = playerTransform.position;
        tmp.y += offsetY;
        transform.position = tmp;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 temp = transform.position;

        temp.x = playerTransform.position.x + offsetX;
        //temp.y = playerTransform.position.y + offsetY; // comment to prevent camera following player on Y-axis
        
        // Camera boundaries 
        if (temp.x >= maxX)
            temp.x = maxX;

        if (temp.x <= minX)
            temp.x = minX;

        if (temp.y >= maxY)
            temp.y = maxY;

        if (temp.y <= minY)
            temp.y = minY;
        transform.position = temp;
    }
}
