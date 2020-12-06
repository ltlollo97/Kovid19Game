using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Molecula : Enemy
{
    public AudioSource hitSound;
    public int xDirection;
    public int yDirection;
    private Vector3 startingPosition;
    [SerializeField] Transform playerCharacter;
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
       
        float distToPlayer = Vector2.Distance(transform.position, playerCharacter.position);

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
    }

    public override void EnemyMove()
    {
        transform.position = new Vector3(moveX, moveY, 0f) + new Vector3(1,0,0) * Mathf.Sin(Time.realtimeSinceStartup) * xDirection;
        
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

    protected void ChasePlayer()
    {
        if (transform.position.x < playerCharacter.position.x)
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
