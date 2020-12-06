﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : Enemy
{
    public AnimationClip explosion;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        health = 400;
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Q) == true)
        {
            Destroy(gameObject);
            anim.Play(explosion.name);
        }
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Attack")
        {
            health -= 50;
            Debug.Log("Hit");
        }
    }

    public override void EnemyMove()
    {
        transform.position *= Time.deltaTime * 0.5f;
    }

    protected override void ChasePlayer()
    {

    }
}

