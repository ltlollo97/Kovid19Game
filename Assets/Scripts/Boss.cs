using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public AudioSource hitSound, laughSound, startSound, bitchSound;
    public AnimationClip explosion;
    public float enemySpeed;
    private int offset = 2;
    public Animator anim;
    protected Player player;
    public int health;
    private bool dead = false;
    private bool facingLeft = false;
    private bool invulnerable = false;
    private int prev_position = 0;
    private int curr_position = 0;
    private bool ready_for_new_step = true;
    private Vector3 position;
    public BarsUI healthBar;
    public BarsUI playerBar;
    private bool moveTowards = false;
    private bool welcomed = false;
    private bool hit = false;
    public bool generateChildren = false;
    private bool givingBirth = false;
    public GameObject child;
    public GameObject protection;
    protected GameObject barrier;
    private bool alreadyPlayed = false;

    // Start is called before the first frame update
    void Start()
    {
        healthBar.SetMaxValue(health);
        healthBar.SetValue(health);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Rona needs to present herself first

        if (!welcomed)
            StartCoroutine(Welcome());

        else
        {
            if (!hit && !givingBirth && !dead)
            {
                FlipRona();

                // PLACE RONA IN 1/5 POSITIONS

                if (!moveTowards)
                    Move();

                // MOVE RONA AGAINST THE PLAYER TO HURT HIM

                if (ready_for_new_step)
                    MoveTowards();
            }
        }

        StartCoroutine(CheckIfWin());
    }


    protected void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("MissRona has collided with " + collision.collider.name);

        if (!invulnerable)
        {
            if (collision.collider.tag == "Attack")
            {
                hit = true;
                GetComponent<Rigidbody2D>().velocity = Vector3.zero; // set to 0 the velocity of MissRona
                health -= 10;
                anim.Play("Hit");
                healthBar.SetValue(health);
                if (!hitSound.isPlaying)
                    hitSound.Play();
                if (health > 0)
                    StartCoroutine(Invulnerability());
                else
                    StartCoroutine(Die());
            }

            if (collision.collider.tag == "Player" && !dead)
            {
                anim.Play("Laugh");
                if (!laughSound.isPlaying)
                    laughSound.Play();
            }
        }
    }

    protected void Flip()
    {
        facingLeft = !facingLeft;
        Vector2 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    protected void FlipRona()
    {
        if (transform.position.x < player.transform.position.x - offset)    //go left
        {
            if (!facingLeft)
                Flip();
        }

        else if (transform.position.x > player.transform.position.x + offset)   //go right
        {
            if (facingLeft)
                Flip();
        }

        else
        {
            // nothing
        }
    }

    protected void Move()
    {
        if (ready_for_new_step)
        {       // If the previous step has been completed, select a new one. Otherwise, continue the step of before.

            while (curr_position == prev_position)      // Select a position with index 1-5 different from the one of before.
            {
                curr_position = Random.Range(1, 5);
                //Debug.Log(curr_position);
            }

            Debug.Log(curr_position);

            ready_for_new_step = false;
        }

        if (curr_position == 1) // BOTTOM-RIGHT
        {
            position.x = 6.5f;
            position.y = -4.5f;
            position.z = 0;

            if (Mathf.Abs(transform.position.x - position.x) < 0.5 && Mathf.Abs(transform.position.y - position.y) < 0.5)
            {
                prev_position = curr_position;
                ready_for_new_step = true;      // Unlock the procedure for a new step!
            }

            else
                transform.position = Vector2.MoveTowards(transform.position, position, enemySpeed * Time.deltaTime);
        }

        else if (curr_position == 2) // BOTTOM-LEFT
        {
            position.x = -8.2f;
            position.y = -4.5f;
            position.z = 0;

            if (Mathf.Abs(transform.position.x - position.x) < 0.5 && Mathf.Abs(transform.position.y - position.y) < 0.5)
            {
                prev_position = curr_position;
                ready_for_new_step = true;      // Unlock the procedure for a new step!
            }

            else
                transform.position = Vector2.MoveTowards(transform.position, position, enemySpeed * Time.deltaTime);
        }

        else if (curr_position == 3) // TOP-RIGHT
        {
            position.x = 6.5f;
            position.y = -1f;
            position.z = 0;

            if (Mathf.Abs(transform.position.x - position.x) < 0.5 && Mathf.Abs(transform.position.y - position.y) < 0.5)
            {
                if (generateChildren)
                    StartCoroutine(Attack());     // Generate new child!
                prev_position = curr_position;
                ready_for_new_step = true;      // Unlock the procedure for a new step!
            }

            else
                transform.position = Vector2.MoveTowards(transform.position, position, enemySpeed * Time.deltaTime);
        }

        else if (curr_position == 4)  // TOP-LEFT
        {
            position.x = -8.2f;
            position.y = -1f;
            position.z = 0;

            if (Mathf.Abs(transform.position.x - position.x) < 0.5 && Mathf.Abs(transform.position.y - position.y) < 0.5)
            {
                if (generateChildren)
                    StartCoroutine(Attack());     // Generate new child!
                prev_position = curr_position;
                ready_for_new_step = true;      // Unlock the procedure for a new step!
            }

            else
                transform.position = Vector2.MoveTowards(transform.position, position, enemySpeed * Time.deltaTime);
        }
    }

    protected void MoveTowards()         // move Rona Towards the Player
    {
        if (!moveTowards)
        {
            moveTowards = true;
            // the target must be fixed, otherwise Player cannot avoid Rona
            position.x = player.transform.position.x;
            position.y = player.transform.position.y;
            position.z = 0;
        }

        if (Mathf.Abs(transform.position.x - position.x) < 0.5 && Mathf.Abs(transform.position.y - position.y) < 0.5)
            moveTowards = false;    // Rona is where the player is!

        else
            transform.position = Vector2.MoveTowards(transform.position, position, 2 * enemySpeed * Time.deltaTime);
    }

    private IEnumerator Die()
    {
        dead = true;
        gameObject.layer = 14; // switch to "ImmuneBoss" layer
        anim.Play("Die");
        yield return new WaitForSeconds(1.8f);        // length of the "Die" animation: 0.667 (+ approx. time for the sound to end)
        Destroy(gameObject);
        // If Rona dies destroy also all her children
        GameObject[] children = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject child in children)
        {
            Destroy(child);
        }
    }

    private void GenerateChildren()
    {
        Instantiate(child, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
    }

    private IEnumerator Invulnerability()
    {
        invulnerable = true;
        gameObject.layer = 14; // switch to "ImmuneBoss" layer
        yield return new WaitForSeconds(1f);
        hit = false;
        Instantiate(protection, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
        yield return new WaitForSeconds(4f);
        barrier = GameObject.Find("Protection(Clone)");
        Destroy(barrier);
        invulnerable = false;
        gameObject.layer = 15; // bring back to "Boss" layer
    }

    private IEnumerator Welcome()
    {
        yield return new WaitForSeconds(0.1f);
        welcomed = true;
        anim.Play("Laugh");
        if (!startSound.isPlaying)
            startSound.Play();
        yield return new WaitForSeconds(1f);
    }

    private IEnumerator Attack()
    {
        givingBirth = true;
        GetComponent<Rigidbody2D>().velocity = Vector3.zero; // set to 0 the velocity of MissRona
        anim.Play("Attack");
        yield return new WaitForSeconds(1f);
        GenerateChildren();
        yield return new WaitForSeconds(1f);
        givingBirth = false;
    }

    private IEnumerator CheckIfWin()
    {
        if (playerBar.GetValue() <= 0 && !alreadyPlayed)
        {
            if (!bitchSound.isPlaying && !laughSound.isPlaying)
            {
                yield return new WaitForSeconds(1.5f);
                bitchSound.Play();
                alreadyPlayed = true;
            }
        }
    }
}