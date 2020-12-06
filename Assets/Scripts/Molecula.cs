using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Molecula : Enemy
{

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        enemySpeed = 1.0f;
        health = 200;
    }

    // Update is called once per frame
    void Update()
    {

        base.Update();
    }

    public override void EnemyMove()
    {
        // transform.position = new Vector3(moveX, moveY, 0f) + new Vector3(1,0,0) * Mathf.Sin(Time.realtimeSinceStartup) * xDirection;

    }


    protected override void ChasePlayer()

    {
        if (transform.position.x < player.transform.position.x) //go left
        {
            if (!facingLeft)
                FlipEnemy();

            GetComponent<Rigidbody2D>().velocity = new Vector2(enemySpeed, 0);

        }

        else //go right
        {
            if (facingLeft)
                FlipEnemy();

            GetComponent<Rigidbody2D>().velocity = new Vector2(-enemySpeed, 0);

        }
    }

}
