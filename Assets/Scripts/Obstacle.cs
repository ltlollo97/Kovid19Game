using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Explodable))]
public class Obstacle : MonoBehaviour
{
    public AudioSource fallingSound;
    public float fallSpeed = 8.0f;
    public float spinSpeed = 250.0f;
    private Explodable _explodable;
    private SoundManagerScript soundManager;
    Rigidbody2D rb;

    // Use this for initialization
    void Start()
    {
        _explodable = GetComponent<Explodable>();
        rb = GetComponent<Rigidbody2D>();
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManagerScript>();
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
            {
                SoundManagerScript.PlaySound("breakingObject");
                touched = true;
            }
            _explodable.explode(); // generates fragments and destroys parent objectt
        }
    }
}