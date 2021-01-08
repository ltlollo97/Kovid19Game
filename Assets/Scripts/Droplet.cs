using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Droplet : Enemy
{
    private int offset = 2;
    protected GameObject rightSide;
    protected GameObject leftSide;
    private Vector2 target;
    private Vector3 position;
    private bool touched;
    public float amplitude;
    private bool waited;
    private bool called = false;

    // Start is called before the first frame update
    new void Start()
    {
        rightSide = GameObject.Find("RightSide");
        leftSide = GameObject.Find("LeftSide");
        base.Start();
        health = 100;
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }

    protected override void ChasePlayer()
    {
        if (Mathf.Abs(transform.position.x - player.transform.position.x) < 3.0 && !touched && !waited)
        {
            if (transform.position.x > player.transform.position.x + 1.5 && facingLeft)
                FlipEnemy();

            else if (transform.position.x < player.transform.position.x - 1.5 && !facingLeft)
                FlipEnemy();

            if (!called)
                StartCoroutine(Wait());

            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemySpeed * Time.deltaTime);
            
        }

        else
        {
            if (facingLeft)
            {
                position.x = rightSide.transform.position.x + 5;
                position.y = transform.position.y;
                position.z = 0;
               // transform.position = Vector2.MoveTowards(transform.position, position, enemySpeed * Time.deltaTime);
                GetComponent<Rigidbody2D>().velocity = new Vector2(enemySpeed, amplitude * Mathf.Sin(Time.time * 3f));
            }

            else if (!facingLeft)
            {
                position.x = leftSide.transform.position.x - 5;
                position.y = transform.position.y;
                position.z = 0;
                //transform.position = Vector2.MoveTowards(transform.position, position, enemySpeed * Time.deltaTime);
                GetComponent<Rigidbody2D>().velocity = new Vector2(-enemySpeed, amplitude * Mathf.Sin(Time.time * 3f));
            }
        }

        if (transform.position.x > rightSide.transform.position.x + 4) //go left
        {
            if (facingLeft)
                FlipEnemy();
        }

        else if (transform.position.x < leftSide.transform.position.x - 4) //go right
        {
            if (!facingLeft)
                FlipEnemy();
        }
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Droplet has collided with " + collision.collider.name);

        if (collision.collider.tag == "Player")
        {
            StartCoroutine(ChangeVariable());
        }

        if (collision.collider.tag == "Attack")
        {
            health -= collision.gameObject.GetComponent<Projectile>().attackValue;

            anim.Play("Hit");
            if (!hitSound.isPlaying)
                hitSound.Play();
            // health -= GetSanitizerAttack(); this should return the attack value of an item
        }
    }

    private IEnumerator ChangeVariable()
    {
        touched = true;
        yield return new WaitForSeconds(3f);
        touched = false;
    }

    private IEnumerator Wait()
    {
        called = true;
        yield return new WaitForSeconds(1f);
        waited = true;
        yield return new WaitForSeconds(3f);
        waited = false;
        called = false;
    }
}