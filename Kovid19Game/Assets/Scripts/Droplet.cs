using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Droplet : Enemy
{

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
            SoundManagerScript.PlaySound("enemy2Hit");
        }
    }


}
