using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour
{
    // Destroys game object
    private float waitTime = 2.0f;
    private float timer = 0.0f;


    void Update()
    {
        timer += Time.deltaTime;

        if((tag == "Attack" || tag == "Effect") && timer > waitTime )
        {
            Destroy(gameObject); 
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag != "Player")
        {
            Destroy(gameObject);
        }

        if(gameObject.tag == "HealthKit" && collision.collider.tag == "Player") //healthkit destroys when player hits it
        {
            Destroy(gameObject);
        }
            
    }
}
