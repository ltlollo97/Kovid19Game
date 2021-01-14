using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public AudioSource sound;
    public float speed;
    public float startX;
    public float endX;
    private Vector3 position;
    private bool flip_positions = false;
    protected Player player;
    public bool flip;

    // Start is called before the first frame update
    void Start()
    { 
        transform.position = new Vector3(startX, -4, 0);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        position = new Vector3(endX, transform.position.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < position.x - 2)
        {
            if (flip)
                flip = !flip;
            GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
            transform.position = Vector2.MoveTowards(transform.position, position, speed * Time.deltaTime);
        }

        else if (transform.position.x > position.x + 2)
        {
            if (flip)
                flip = !flip;
            GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, 0);
            transform.position = Vector2.MoveTowards(transform.position, position, speed * Time.deltaTime);
        }

        else
        {
            if (!flip)
            {

                if (!flip_positions)
                {
                    position = new Vector3(startX, transform.position.y, 0);
                    flip_positions = true;
                }

                else
                {
                    position = new Vector3(endX, transform.position.y, 0);
                    flip_positions = false;
                }
            }

        }

        Destroy(gameObject, 7);
    }

    void OnBecameVisible() 
    {
        if (!sound.isPlaying)
            sound.Play();
    }

    // void WaitAndDestroy()
    // {
    //     yield WaitForSeconds(3.0);
    //     Destroy(gameObject);
    // }
}
