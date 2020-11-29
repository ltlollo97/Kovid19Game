using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int playerSpeed = 10;
    public int jumpPower = 1250;
    public GameObject sanitizerPuffPrefab;
    
    

    //player status
    private bool facingLeft = false; //player orientation
    private float moveX; //horizontal movement
    private bool isGrounded = true; //if true player is not jumping 
    private bool ultraReady = false;
    private int health;
    private Animator playerAnimator;
    private ScoreSystem level;
    private float ultimateAttackCooldown = 60f;

    // Start is called before the first frame update
    void Start()
    {
        health = 250;
        // health = base + mask power up

        GameObject camObj = GameObject.FindGameObjectWithTag("MainCamera");
        level = camObj.GetComponent<ScoreSystem>();
        playerAnimator = gameObject.GetComponent<Animator>();

        Debug.Log("Initial health = " + health);
        
    }


    // Update is called once per frame
    void Update()
    {
        PlayerMove();

        if (level.GetTimeSpent() > ultimateAttackCooldown)
        {
            ultraReady = true;
            ultimateAttackCooldown = 60f; //restore cooldown
        }
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
        //controls
        moveX = Input.GetAxis("Horizontal");
        
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded == true)  //up arrow to jump IF the player has not jumped already
        {
            Jump();
        }

        if(Input.GetKeyDown(KeyCode.Space) == true) //player hits space bar to attack
        {
            playerAnimator.Play("Attack");
            Attack();
            
        }

        if(Input.GetKeyDown(KeyCode.Q) == true && ultraReady == true) //ultra attack available,  player hits 'q' to perform an ultra attack
        {
            UltraAttack();
        }
       
        //direction
        if (moveX < 0.0f && facingLeft == false)
        {
            FlipPlayer();
        }
        else if(moveX > 0.0f && facingLeft == true)
        {
            FlipPlayer();
        }
        //physics
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * playerSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
    }

    private void Jump()
    {
       //jumping code
       GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpPower);
       isGrounded = false;  
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
        
        GameObject attack = Instantiate(sanitizerPuffPrefab, 
                                transform.position + new Vector3(1.5f,0.5f,0), 
                                Quaternion.identity) as GameObject;
        attack.GetComponent<Rigidbody2D>().velocity = new Vector2(4f, 0);
        
    }

    private void UltraAttack()
    {
        //instantiate attack component
        //give it speed
        //play animation
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Player has collided with " + collision.collider.name);
        if(collision.gameObject.tag == "Floor")
        {
            isGrounded = true;
        }

        if(collision.gameObject.tag == "Enemy" && !isGrounded)
        {
            isGrounded = true; //in this way jump is not bugged
            health -= 20; 
        }

        if(collision.gameObject.tag == "HealthKit")
        {
            if(health < 250)
            {
                // max value : base - current
                health += 100;

                if (health > 250)
                    health = 250;
            }
                
            Debug.Log("New health: " + health);
        }

        
    }
}
