using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Molecula : MonoBehaviour
{
    protected GameObject rightSide;
    protected GameObject leftSide;
    private Vector3 position;
    private bool speedup = false;
    private int state = 1;
    public AudioSource hitSound, appearSound;
    public float enemySpeed;
    private int health;
    private SoundManagerScript soundManager;
    public GameObject smoke;
    protected bool facingLeft = false;
    protected Player player;
    protected Animator anim;
    private bool addDeath = false;
    private bool go = false;
    private bool dead = false;

    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        anim = GetComponent<Animator>();
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        rightSide = GameObject.Find("RightSide");
        leftSide = GameObject.Find("LeftSide");
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManagerScript>();
        health = 150;
        StartCoroutine(Go());
    }

    // Update is called once per frame
    private void Update()
    {
        if (go)
        {
            if (state == 1)
                MoveTowardsPlayer();
            else if (state == 2)
                ReachBorder();
            else if (state == 3)
                Escape();

            CheckIfDied();

            Debug.Log(state);
        }
    }

    protected void MoveTowardsPlayer()
    {
        position = new Vector3(player.transform.position.x, transform.position.y, 0);

        if (Mathf.Abs(transform.position.x - player.transform.position.x) < 1.5)
            state = 2;

        transform.position = Vector2.MoveTowards(transform.position, position, enemySpeed * Time.deltaTime);
    }

    protected void ReachBorder()
    {
        if (facingLeft)
        {
            position.x = rightSide.transform.position.x + 1.5f;
            position.y = transform.position.y;
            position.z = 0;
            transform.position = Vector2.MoveTowards(transform.position, position, enemySpeed * Time.deltaTime);

            if (Mathf.Abs(transform.position.x - position.x) < 0.2) //go left
            {
                FlipEnemy();
                anim.SetBool("Hit", false);
                if (speedup)
                {
                    enemySpeed /= 2;
                    speedup = false;
                }
                state = 1;        //  2 --> 1
            }
        }

        else if (!facingLeft)
        {
            position.x = leftSide.transform.position.x - 1.5f;
            position.y = transform.position.y;
            position.z = 0;
            transform.position = Vector2.MoveTowards(transform.position, position, enemySpeed * Time.deltaTime);

            if (Mathf.Abs(transform.position.x - position.x) < 0.2) //go right
            {
                FlipEnemy();
                anim.SetBool("Hit", false);
                if (speedup)
                {
                    enemySpeed /= 2;
                    speedup = false;
                }
                state = 1;          //  2 --> 1
            }
        }
    }

    protected void Escape()
    {
        if (facingLeft && player.transform.position.x > transform.position.x + 1 || !facingLeft && player.transform.position.x < transform.position.x + 1)
            FlipEnemy();
        if (!speedup)
        {
            enemySpeed *= 2;
            speedup = true;
        }
        state = 2;
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Droplet has collided with " + collision.collider.name);

        if (collision.collider.tag == "Player")
        {
            if (!speedup)
            {
                enemySpeed *= 2;
                speedup = true;
            }
        }

        if (collision.collider.tag == "Attack")
        {
            health -= collision.gameObject.GetComponent<Projectile>().attackValue;
            //    anim.Play("Hit");
            anim.SetBool("Hit", true);
            if (!hitSound.isPlaying)
                hitSound.Play();
            if (health > 0)
            {
                StartCoroutine(Wait());   // 1 --> 3
            }
            else
                state = 0;
        }

        if (collision.collider.tag == "Enemy" && collision.collider.gameObject.layer == 17)
        {
            anim.SetBool("Hit", false);
            if (speedup)
            {
                enemySpeed /= 2;
                speedup = false;
            }

            if (facingLeft && player.transform.position.x < transform.position.x + 1 || !facingLeft && player.transform.position.x > transform.position.x + 1)
                FlipEnemy();

            state = 1;
        }
    }

    protected void CheckIfDied()
    {
        if (health <= 0 && !addDeath)
        {
            if (!hitSound.isPlaying)
                hitSound.Play();

            EnemyTracker tracker = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<EnemyTracker>();
            tracker.AddDeath();
            addDeath = true;

            StartCoroutine(Die());
        }
    }

    protected void FlipEnemy()
    {
        facingLeft = !facingLeft;
        Vector2 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private IEnumerator Go()
    {
        if (!appearSound.isPlaying && Mathf.Abs(transform.position.x - player.transform.position.x) < 15)
            appearSound.Play();
        yield return new WaitForSeconds(1f);
        if (facingLeft && player.transform.position.x < transform.position.x + 1 || !facingLeft && player.transform.position.x > transform.position.x + 1)
            FlipEnemy();
        go = true;
    }

    private IEnumerator Die()
    {
        anim.Play("Die");
        gameObject.layer = 14; // switch to "ImmuneBoss" layer
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        go = false;
        yield return new WaitForSeconds(1f);
        if (!dead)
        {
            SoundManagerScript.PlaySound("puff");
            dead = true;
        }
        Instantiate(smoke, new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity);
        Destroy(gameObject);
    }

    private IEnumerator Wait()
    {
        anim.Play("Die");
        gameObject.layer = 14; // switch to "ImmuneBoss" layer
        state = 0;
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        yield return new WaitForSeconds(1f);
        state = 3;
        yield return new WaitForSeconds(2f);
        gameObject.layer = 17; // switch to "Enemy" layer   
        anim.SetBool("Hit", false);
    }
}