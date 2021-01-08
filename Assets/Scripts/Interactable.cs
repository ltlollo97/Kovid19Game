using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    // Destroys game object
    private float waitTime = 1.0f;
    private float timer = 0.0f;
    //public AudioSource clip;

    private void Start()
    {
        //if(gameObject.tag == "HealthKit")
            //clip = GameObject.Find("Camera Obj").GetComponent<AudioSource>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if ((tag == "Attack" || tag == "Effect") && timer > waitTime)
        {
            Destroy(gameObject);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag != "Player")
            Destroy(gameObject);

        if (gameObject.tag == "HealthKit" && collision.collider.tag == "Player") //healthkit destroys when player hits it
        {
            SoundManagerScript.PlaySound("health");
            Destroy(gameObject);
        }

    }
}
