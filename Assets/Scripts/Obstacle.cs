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
    private int coin;
    private bool called = false;
    private bool activated = false;
    protected Player player;
    Rigidbody2D rb;

    // Use this for initialization
    void Start()
    {
        _explodable = GetComponent<Explodable>();
        rb = GetComponent<Rigidbody2D>();
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManagerScript>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        if (!called && !activated)
            IsBelow();      // each frame verifies if the Player is below
    }


    void IsBelow()
    {
        if (Mathf.Abs(transform.position.x - player.transform.position.x) < 2.0 && transform.position.y > player.transform.position.y + 4)
        {
            StartCoroutine(Activate());
            coin = Random.Range(1, 10);       // toss a coin

            if (coin <= 5)
            {
                called = true;
                rb.isKinematic = false;
                gameObject.tag = "Object";
                gameObject.layer = 11;

                if (!fallingSound.isPlaying)
                    fallingSound.Play();

                transform.Translate(Vector3.down * fallSpeed * Time.deltaTime, Space.World);
                transform.Rotate(Vector3.forward, spinSpeed * Time.deltaTime);
            }
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

    private IEnumerator Activate()
    {
        activated = true;
        yield return new WaitForSeconds(5f);
        activated = false;
    }
}