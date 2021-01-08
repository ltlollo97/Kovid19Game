using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Molecula : Enemy
{
    private int offset = 2;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        //enemySpeed = 1.0f;
        health = 200;
    }

    // Update is called once per frame
    new void Update()
    {

        base.Update();
    }
    

    protected override void ChasePlayer()

    {
        if (transform.position.x < player.transform.position.x - offset) //go left
        {
            if (!facingLeft)
                FlipEnemy();

            GetComponent<Rigidbody2D>().velocity = new Vector2(enemySpeed, 0);
        }

        else if (transform.position.x > player.transform.position.x + offset) //go right
        {
            if (facingLeft)
                FlipEnemy();

            GetComponent<Rigidbody2D>().velocity = new Vector2(-enemySpeed, 0);
        }

        else
        {
            // nothing
        }
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Attack")
        {
            health -= collision.gameObject.GetComponent<Projectile>().attackValue;

            anim.Play("Hit");
            if (!hitSound.isPlaying)
                hitSound.Play();
            // health -= GetSanitizerAttack(); this should return the attack value of an item
        }
    }
}
