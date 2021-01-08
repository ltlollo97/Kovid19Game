using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public AudioSource hitSound;
    public float speed;
    public float startX;
    public float startY;
    public float endX;
    private Vector3 position;
    private bool flip_positions = false;
    protected Player player;
    public float limitSound;
    private bool flip = false;

    // Start is called before the first frame update
    void Start()
    { 
        transform.position = new Vector3(startX, startY, 0);
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
            GetComponent<Rigidbody2D>().velocity = new Vector2(speed, Mathf.Sin(Time.time * 3f) * 15f);
            transform.position = Vector2.MoveTowards(transform.position, position, speed * Time.deltaTime);
        }

        else if (transform.position.x > position.x + 2)
        {
            if (flip)
                flip = !flip;
            GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, Mathf.Sin(Time.time * 3f) * 15f);
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
    }

    protected void OnCollisionEnter2D(Collision2D collision){
        if (collision.collider.name == "Floor")
        {
            if (!hitSound.isPlaying && Mathf.Abs(transform.position.x - player.transform.position.x) < limitSound)
                hitSound.Play();
        }
    }
}
