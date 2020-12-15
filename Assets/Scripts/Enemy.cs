﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected float moveX;
    protected float moveY;

    public AudioSource hitSound;
    public float enemySpeed;
    protected int health;
    protected bool facingLeft = false;
    protected float minDistance = 0.2f;
    protected Player player;
    protected float playerUltraCooldown;
    protected Animator anim;

    // Start is called before the first frame update
    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    public void Update()
    {
        ChasePlayer();

        if (health <= 0)
        {
            if (hitSound.isPlaying)
                Invoke("Destroy(gameObject)", 1);
            else
            {
                EnemyTracker tracker = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<EnemyTracker>();
                tracker.AddDeath();
                anim.Play("Die");
                Destroy(gameObject);
            }
                

            player.SetUltraCooldown(player.GetUltraCooldown() - 2f); //if enemy is killed, reduce player's ultra cooldown
        }

    }


    public abstract void EnemyMove();

    protected abstract void ChasePlayer();

    public int RandomExcept(int min, int max, int except)
    {
        int random = Random.Range(min, max);
        if (random == 0) random = 1;
        return random;
    }

    protected void FlipEnemy()
    {
        facingLeft = !facingLeft;
        Vector2 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Attack")
        {
            health -= collision.gameObject.GetComponent<Projectile>().attackValue;
            //Debug.Log("Hit");
            anim.Play("Hit");
            if (!hitSound.isPlaying)
                hitSound.Play();
            // health -= GetSanitizerAttack(); this should return the attack value of an item
        }
    }
}
