using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    private GameObject player;
    public float xMin;
    public float xMax;
    public float yMin;
    public float yMax;
    public Vector3 offset;


    public float smoothSpeed = 0.125f;
   
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 desiredPos = player.transform.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPos, smoothSpeed);
        transform.position = smoothedPosition; // Camera follows the player with specified offset position
    }
}
