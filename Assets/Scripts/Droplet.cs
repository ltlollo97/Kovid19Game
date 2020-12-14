using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Droplet : Enemy
{
 
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

    public override void EnemyMove()
    {
        //
    }


    protected override void ChasePlayer()
    {
        if (transform.position.x < player.transform.position.x) //go left
        {
            if (!facingLeft)
                FlipEnemy();

            GetComponent<Rigidbody2D>().velocity = new Vector2(enemySpeed, Mathf.Sin(Time.time * 3f) * 2f);
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

}
