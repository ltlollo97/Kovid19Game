using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Droplet : Enemy
{
    private int offset = 2;
    private Vector2 target;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        //enemySpeed = 1.5f;
        health = 100;
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

            GetComponent<Rigidbody2D>().velocity = new Vector2(enemySpeed, Mathf.Sin(Time.time * 3f) * 2f);
        }

        else if (transform.position.x > player.transform.position.x + offset) //go right
        {
            if (facingLeft)
                FlipEnemy();
        
            GetComponent<Rigidbody2D>().velocity = new Vector2(-enemySpeed, Mathf.Sin(Time.time * 3f) * 2f);
        }

        else
        {
            // nothing
        }

        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemySpeed * Time.deltaTime);
    }
}
