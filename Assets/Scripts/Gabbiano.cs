using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gabbiano : MonoBehaviour
{
    public float startPoint;
    public float endPoint;
    public AudioSource sound;
    private float temp;
    public float speed;
    private bool flip_positions = false;
    private bool flip = false;
    public bool needToFlip = false;
    protected Player player;
    public float angle;
    private Vector3 position;
    private bool rotate = false;
    public GameObject shit;
    private bool activated = false;

    // Start is called before the first frame update
    void Start()
    {
        Quaternion target = Quaternion.Euler(0, 0, angle);
        transform.position = new Vector3(startPoint, transform.position.y, 0);
        position = new Vector3(endPoint, transform.position.y, 0);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        Translate(ref position);
        Rotate();
    }

    protected void Translate(ref Vector3 position)
    {
        if (Mathf.Abs(transform.position.x - position.x) > 0.2)
            transform.position = Vector2.MoveTowards(transform.position, position, speed * Time.deltaTime);

        else
        {
            if (needToFlip)
                Flip();
            Swap(ref startPoint, ref endPoint);
            activated = false;      // now he can shit again if he wants
            position = new Vector3(endPoint, transform.position.y, 0);
        }
    }

    protected void Rotate()
    {
        if (!rotate)
        {
            Quaternion target = Quaternion.Euler(0, 0, angle);

            if (transform.rotation != target)
                transform.rotation = Quaternion.Slerp(transform.rotation, target, speed * Time.deltaTime);
            else
                rotate = true;
        }

        else
        {
            Quaternion target = Quaternion.Euler(0, 0, 0);
            if (transform.rotation != target)
                transform.rotation = Quaternion.Slerp(transform.rotation, target, speed * Time.deltaTime);
            else
                rotate = false;
        }
    }

    protected void Flip()
    {
        Vector2 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    protected void Swap(ref float startPoint, ref float endPoint)
    {
        float temp = startPoint;
        startPoint = endPoint;
        endPoint = temp;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name.Equals("Player"))
        {
            if (!activated)
            {
                Instantiate(shit, new Vector3(transform.position.x, transform.position.y - 1, 0), Quaternion.identity);
                if (!sound.isPlaying)
                    sound.Play();
                activated = true;
            }
        }
    }
}
