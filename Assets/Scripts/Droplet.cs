using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Droplet : MonoBehaviour
{
    protected GameObject rightSide;
    protected GameObject leftSide;
    private Vector3 position;
    public float amplitude;
    private bool speedup = false;
    private int state = 1;
    public AudioSource hitSound, appearSound;
    public float enemySpeed;
    public int health;
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
        health = 100;
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
        }
    }

    protected void MoveTowardsPlayer()
    {
        if (Mathf.Abs(transform.position.x - player.transform.position.x) < 1.5)
            state = 2;

        if (facingLeft)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemySpeed * Time.deltaTime);
            GetComponent<Rigidbody2D>().velocity = new Vector2(enemySpeed, amplitude * Mathf.Sin(Time.time * 3f));
        }

        else if (!facingLeft)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemySpeed * Time.deltaTime);
            GetComponent<Rigidbody2D>().velocity = new Vector2(-enemySpeed, amplitude * Mathf.Sin(Time.time * 3f));
        }
    }

    protected void ReachBorder()
    {

        if (facingLeft)
        {
            position.x = rightSide.transform.position.x + 5;
            position.y = rightSide.transform.position.y;
            position.z = 0;
            transform.position = Vector2.MoveTowards(transform.position, position, enemySpeed * Time.deltaTime);
            GetComponent<Rigidbody2D>().velocity = new Vector2(enemySpeed, amplitude * Mathf.Sin(Time.time * 3f));

            if (transform.position.x > rightSide.transform.position.x + 4) //go left
            {
                FlipEnemy();
                anim.SetBool("Hit", false);
                if (speedup)
                {
                    enemySpeed /= 2;
                    speedup = false;
                }
                transform.position = new Vector3(rightSide.transform.position.x + 4, rightSide.transform.position.y + Random.Range(-2,2), 0);
                StartCoroutine(WaitaBit());
                state = 1;        //  2 --> 1
            }
        }

        else if (!facingLeft)
        {
            position.x = leftSide.transform.position.x - 5;
            position.y = leftSide.transform.position.y;
            position.z = 0;
            transform.position = Vector2.MoveTowards(transform.position, position, enemySpeed * Time.deltaTime);
            GetComponent<Rigidbody2D>().velocity = new Vector2(-enemySpeed, amplitude * Mathf.Sin(Time.time * 3f));

            if (transform.position.x < leftSide.transform.position.x - 4) //go right
            {
                FlipEnemy();
                anim.SetBool("Hit", false);
                if (speedup)
                {
                    enemySpeed /= 2;
                    speedup = false;
                }
                transform.position = new Vector3(leftSide.transform.position.x - 4, leftSide.transform.position.y + Random.Range(-2, 5), 0);
                StartCoroutine(WaitaBit());
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
                StartCoroutine(Invulnerability());  // 1 --> 3
            else
                state = 0;
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

    private IEnumerator Invulnerability()
    {
        gameObject.layer = 14; // switch to "ImmuneBoss" layer
        state = 0;
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        yield return new WaitForSeconds(1f);
        state = 3;              // 1 --> 3
        yield return new WaitForSeconds(2f);
        gameObject.layer = 9; // switch to "Enemy" layer
        anim.SetBool("Hit", false);
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
        anim.Play("Hit");
        gameObject.layer = 14; // switch to "ImmuneBoss" layer
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        go = false;
        yield return new WaitForSeconds(1f);
        if (!dead)
        {
            SoundManagerScript.PlaySound("puff");
            dead = true;
        }
        Instantiate(smoke, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
        Destroy(gameObject);
    }

    private IEnumerator WaitaBit()
    {
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        go = false;
        state = 1;
        yield return new WaitForSeconds(Random.Range(1f, 5f));
        go = true;
    }
}