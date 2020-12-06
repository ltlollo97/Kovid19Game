using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Molecula : Enemy
{
    public int xDirection;
    public int yDirection;
    private Vector3 startingPosition;
    [SerializeField]
    public Transform Player;
    public float aggroRange = 6f;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        enemySpeed = 0.5f;
        health = 200;
        xDirection = RandomExcept(-1, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
       
        float distToPlayer = (transform.position.x - Player.position.x);

        if (distToPlayer < 0)
        {
            //move towards player
            ChasePlayer();
        }
        else
        {
            //move casually
          //  EnemyMove();
        }

        if (health == 0)
        {
            Destroy(gameObject);

            player.SetUltraCooldown(player.GetUltraCooldown() - 2f); //if enemy is killed, reduce player's ultra cooldown

            Debug.Log(player.GetUltraCooldown());
        }
    }

    public override void EnemyMove()
    {
        transform.position = new Vector3(moveX, moveY, 0f) + new Vector3(1,0,0) * Mathf.Sin(Time.realtimeSinceStartup) * xDirection;
        
    }

    protected void ChasePlayer()
    {
        if (transform.position.x < Player.position.x)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(enemySpeed, 0);
            Vector2 localScale = gameObject.transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }

        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(enemySpeed, 0);
            Vector2 localScale = gameObject.transform.localScale;
            localScale.x *= 1;
            transform.localScale = localScale;
        }
    }

}
