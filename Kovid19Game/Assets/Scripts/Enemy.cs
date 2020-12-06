using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected float moveX;
    protected float moveY;
    protected float enemySpeed;
    protected int health;
    protected bool facingLeft = false;

    protected Player player;
    protected float playerUltraCooldown;

    // Start is called before the first frame update
    public void Start()
    {
        //get enemy initial position
        moveX = gameObject.transform.position.x;
        moveY = gameObject.transform.position.y;

        GameObject playObj = GameObject.FindGameObjectWithTag("Player");
        player = playObj.GetComponent<Player>();

    }

    // Update is called once per frame
    public void Update()
    {
        EnemyMove();

        if (health == 0)
        {
            Destroy(gameObject);

            player.SetUltraCooldown(player.GetUltraCooldown() - 2f); //if enemy is killed, reduce player's ultra cooldown

            Debug.Log(player.GetUltraCooldown());

        }

    }


    public abstract void EnemyMove();

    public int RandomExcept(int min, int max, int except)
    {
        int random = Random.Range(min, max);
        if (random == 0) random = 1;
        return random;
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Attack")
        {
            health -= 50;
            Debug.Log("Hit");
            // health -= GetSanitizerAttack(); this should return the attack value of an item
        }
    }

    protected void FlipEnemy()
    {
        facingLeft = !facingLeft;
        Vector2 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }



}
