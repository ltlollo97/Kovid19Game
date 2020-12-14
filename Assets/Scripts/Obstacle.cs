using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Obstacle : MonoBehaviour
{
    public AudioSource fallingSound, hitSound;
    public float fallSpeed = 8.0f;
    public float spinSpeed = 250.0f;
    Rigidbody2D rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

        void OnTriggerEnter2D(Collider2D col)
        {
            bool touched = false;

            if (col.gameObject.name.Equals("Player"))
            {
                rb.isKinematic = false;
                if (!fallingSound.isPlaying && !touched)
                {
                    fallingSound.Play();
                    touched = true;
                }
                transform.Translate(Vector3.down * fallSpeed * Time.deltaTime, Space.World);
                transform.Rotate(Vector3.forward, spinSpeed * Time.deltaTime);
            }
        }

    void OnCollisionEnter2D(Collision2D col)
    {
        bool touched = false;

        if (col.gameObject.name.Equals("Player"))
            Debug.Log("Got you!");
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Floor")
        {
            if (!touched)
                hitSound.Play();
            Destroy(gameObject, 0.5f);
        }
    }
}