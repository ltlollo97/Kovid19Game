using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Droplet : Enemy
{
<<<<<<< HEAD
    public AudioSource hitSound;
=======

>>>>>>> f068c2468cfb6fd05a6cf35cd72f0ce68deb6a86
    public int xDirection;
    public int yDirection;
    private Vector3 startingPosition;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        enemySpeed = 1.5f;
        health = 100;
        yDirection = RandomExcept(-1,1,0);
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
<<<<<<< HEAD


        if (health <= 0)
        {
            if (hitSound.isPlaying)
                Invoke("Destroy(gameObject)", 1);
            else
                Destroy(gameObject);

            player.SetUltraCooldown(player.GetUltraCooldown() - 2f); //if enemy is killed, reduce player's ultra cooldown

            Debug.Log(player.GetUltraCooldown());
        }
=======
>>>>>>> f068c2468cfb6fd05a6cf35cd72f0ce68deb6a86
    }

    public override void EnemyMove()
    {
        transform.position = new Vector3(moveX,moveY,0f) + Vector3.up * Mathf.Sin(Time.realtimeSinceStartup) * yDirection;
    }


    protected override void ChasePlayer()
    {
        if (transform.position.x < player.transform.position.x) //go left
        {
            if (!facingLeft)
               FlipEnemy();

            GetComponent<Rigidbody2D>().velocity = new Vector2(enemySpeed,  Mathf.Sin(Time.time * 3f) * 2f);
           // transform.position += transform.right * Mathf.Sin(Time.time * 3f) * 1f;

        }

        else //go right
        {
            if (facingLeft)
                FlipEnemy();

            GetComponent<Rigidbody2D>().velocity = new Vector2(-enemySpeed, Mathf.Sin(Time.time * 3f) * 2f);
            //transform.position += -transform.right * Mathf.Sin(Time.time * 3f) * 1f;
        }
    }

    new public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Attack")
        {
            health -= 50;
            Debug.Log("Hit");
            if (!hitSound.isPlaying)
                hitSound.Play();
        }
    }
<<<<<<< HEAD
=======


>>>>>>> f068c2468cfb6fd05a6cf35cd72f0ce68deb6a86
}
