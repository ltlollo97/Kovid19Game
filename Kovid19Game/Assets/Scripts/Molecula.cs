using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Molecula : Enemy
{
<<<<<<< HEAD
    public AudioSource hitSound;
    public int xDirection;
    public int yDirection;
    private Vector3 startingPosition;
    [SerializeField] Transform playerCharacter;
    public float aggroRange = 6f;
=======
    
>>>>>>> f068c2468cfb6fd05a6cf35cd72f0ce68deb6a86

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

<<<<<<< HEAD
        if (distToPlayer < aggroRange)
        {
            //move towards player
            ChasePlayer();
        }
        else
        {
            //move casually
            EnemyMove();
        }

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
        base.Update();
>>>>>>> f068c2468cfb6fd05a6cf35cd72f0ce68deb6a86
    }

    public override void EnemyMove()
    {
       // transform.position = new Vector3(moveX, moveY, 0f) + new Vector3(1,0,0) * Mathf.Sin(Time.realtimeSinceStartup) * xDirection;
        
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
