using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Droplet : Enemy
{
    public AudioSource hitSound;
    public int xDirection;
    public int yDirection;
    private Vector3 startingPosition;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        enemySpeed = 1;
        health = 100;
        yDirection = RandomExcept(-1,1,0);
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();


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
        transform.position = new Vector3(moveX,moveY,0f) + Vector3.up * Mathf.Sin(Time.realtimeSinceStartup) * yDirection;
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
}
