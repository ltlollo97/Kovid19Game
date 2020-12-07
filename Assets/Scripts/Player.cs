﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public AudioSource jumpSound, hitSound, spraySound;
    public int playerSpeed = 10;
    public int jumpPower = 1250;
    public GameObject sanitizerPuffPrefab;
    public GameObject sanitizerUltraPrefab;
    public Transform projectileSpawnPoint;
    public BarsUI superAttackBar, healthBar;

    //player status
    private bool facingLeft = false; //player orientation
    private float moveX; //horizontal movement
    private bool isGrounded = true; //if true player is not jumping 
    private bool ultraReady = false;
    private bool isDead = false;
    public int health;
    private bool invulnerable = false;
    private float invincibilityTime = 3f;
    private Animator playerAnimator;
    private ScoreSystem level;
    private GameObject gameOverPanel;
    private float ultimateAttackCooldown = 10f;
    private float nextUltimateFire;


    // Start is called before the first frame update
    void Start()
    {
        //health = 250;
        // health = base + mask power up

        GameObject camObj = GameObject.FindGameObjectWithTag("MainCamera");

        gameOverPanel = GameObject.Find("GameOverPanel");
        gameOverPanel.SetActive(false);

        level = camObj.GetComponent<ScoreSystem>();
        playerAnimator = gameObject.GetComponent<Animator>();

        nextUltimateFire = ultimateAttackCooldown; // wait 60 secs at the beginning

        // set initial values for UI bars
        superAttackBar.SetMaxValue((int)ultimateAttackCooldown);
        superAttackBar.SetValue(0);//when filled up completely, the playe can cast an ultra attack
        healthBar.SetMaxValue(health);
    }


    // Update is called once per frame
    void Update()
    {
        PlayerMove();

        if (Time.time > nextUltimateFire + 1)
        {
            ultraReady = true;

        }

        if (health <= 0)
        {
            isDead = true;
            Die();
            //GameOverPanel.setActive(true);
        }

        //updates Sliders value
        if (!ultraReady)
            superAttackBar.SetValue((int)Time.time % (int)(ultimateAttackCooldown + 1));

        if (!isDead)
            healthBar.SetValue(health);
    }

    public float GetUltraCooldown()
    {
        return ultimateAttackCooldown;
    }

    public void SetUltraCooldown(float val)
    {
        ultimateAttackCooldown = val;
    }

    private void PlayerMove()
    {

        if (isGrounded)
        {
            playerAnimator.SetBool("isJumping", false);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded == true)  //up arrow to jump IF the player has not jumped already
        {
            playerAnimator.SetTrigger("takeOff");
            Jump();
            if (!jumpSound.isPlaying)
                jumpSound.Play();
        }

        if (Input.GetKeyDown(KeyCode.Space) == true) //player hits space bar to attack
        {

            playerAnimator.Play("Attack");
            Attack();
            if (!spraySound.isPlaying)
                spraySound.Play();
        }

        if (Input.GetKeyDown(KeyCode.Q) == true && ultraReady == true) //ultra attack available,  player hits 'q' to perform an ultra attack
        {
            ultraReady = false;
            UltraAttack();
        }

        //direction
        if (moveX < 0.0f && facingLeft == false)
        {
            FlipPlayer();
        }
        else if (moveX > 0.0f && facingLeft == true)
        {
            FlipPlayer();
        }

        moveX = Input.GetAxis("Horizontal");
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * playerSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);

        if (moveX == 0) //player is not moving
        {
            playerAnimator.SetBool("isRunning", false);
        }
        else
        {
            playerAnimator.SetBool("isRunning", true);
        }
    }

    private void Jump()
    {
        isGrounded = false;
        playerAnimator.SetBool("isJumping", true);
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpPower);

    }

    private void FlipPlayer()
    {
        facingLeft = !facingLeft;
        Vector2 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void Attack()
    {

        GameObject attack = Instantiate(sanitizerPuffPrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);

        if (facingLeft)
        {
            Vector2 localScale = attack.transform.localScale;
            localScale.x *= -1;
            attack.transform.localScale = localScale;
            attack.GetComponent<Rigidbody2D>().velocity = new Vector2(4f * -projectileSpawnPoint.right.x, 0);
        }
        else
        {
            attack.GetComponent<Rigidbody2D>().velocity = new Vector2(4f * projectileSpawnPoint.right.x, 0);
        }


    }

    private void UltraAttack()
    {
        //instantiate attack component
        GameObject attack = Instantiate(sanitizerUltraPrefab, projectileSpawnPoint.position + new Vector3(0.0f, 1.0f, 0.0f), projectileSpawnPoint.rotation);

        if (facingLeft)
        {
            Vector2 localScale = attack.transform.localScale;
            localScale.x *= -1;
            attack.transform.localScale = localScale;
            attack.GetComponent<Rigidbody2D>().velocity = new Vector2(4f * -projectileSpawnPoint.right.x, 0);
        }
        else
        {
            attack.GetComponent<Rigidbody2D>().velocity = new Vector2(4f * projectileSpawnPoint.right.x, 0);
        }

        //play animation
        // attack.GetComponent<Animator>().Play("SuperAttackProj");
        nextUltimateFire = Time.time + ultimateAttackCooldown; //reset cooldown 
        superAttackBar.SetValue(0);

    }

    private void Die()
    {
        //play Die animation
        Destroy(gameObject);
        //load Defeat Scene
        Debug.Log("Player is Dead");
        gameOverPanel.SetActive(true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Player has collided with " + collision.collider.name);

        if (collision.gameObject.tag == "Floor")
        {

            isGrounded = true; //in this way jump is not bugged

        }

        if (!invulnerable)
        {

            if ((collision.gameObject.tag == "Enemy" && !isGrounded) || collision.gameObject.tag == "Enemy")
            {
                isGrounded = true; //in this way jump is not bugged
                playerAnimator.Play("Hit");
                if (!hitSound.isPlaying)
                {
                    hitSound.Play();
                }
                StartCoroutine(Invulnerability(collision));
                health -= 20;

            }

            if (collision.gameObject.tag == "HealthKit")
            {
                if (health < 250)
                {
                    // max value : base - current
                    health += 100;

                    if (health > 250)
                        health = 250;
                }

            }
        }
    }

    private IEnumerator Invulnerability(Collision2D collider)
    {
        invulnerable = true;
        gameObject.layer = 10;
        yield return new WaitForSeconds(invincibilityTime);
        gameObject.layer = 8;
        //playerAnimator.SetTrigger("frameEnd");
        invulnerable = false;
    }


}
