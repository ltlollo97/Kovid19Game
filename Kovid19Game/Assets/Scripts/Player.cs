using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
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
    private int health;
    private Animator playerAnimator;
    private ScoreSystem level;
    private float ultimateAttackCooldown = 10f;
    private float nextUltimateFire;

    // Start is called before the first frame update
    void Start()
    {
        health = 250;
        // health = base + mask power up

        GameObject camObj = GameObject.FindGameObjectWithTag("MainCamera");
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

        if(Time.time > nextUltimateFire+1)
        {
            ultraReady = true;
             
        }

        if (health <= 0)
        {
            isDead = true;
            Die();
        }

        //updates Sliders value
        if(!ultraReady)
            superAttackBar.SetValue((int)Time.time % (int) (ultimateAttackCooldown+1));

        if(!isDead)
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
        
        if(isGrounded)
        {
            playerAnimator.SetBool("isJumping", false);
        }
        
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded == true)  //up arrow to jump IF the player has not jumped already
        {
            playerAnimator.SetTrigger("takeOff");
            Jump();
            SoundManagerScript.PlaySound("jump");    
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
      
        moveX = Input.GetAxis("Horizontal");
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * playerSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);

        if(moveX == 0) //player is not moving
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

        if(facingLeft)
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
        //give it speed
        //play animation
        nextUltimateFire = Time.time + ultimateAttackCooldown; //reset cooldown 
        superAttackBar.SetValue(0);

    }

    private void Die()
    {
        //play Die animation
        Destroy(gameObject);
        //load Defeat Scene
        Debug.Log("Player is Dead");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Player has collided with " + collision.collider.name);
        if(collision.gameObject.tag == "Floor")
        {
            isGrounded = true;
        }

        if((collision.gameObject.tag == "Enemy" && !isGrounded) || collision.gameObject.tag == "Enemy")
        {
            isGrounded = true; //in this way jump is not bugged
            health -= 20;
            SoundManagerScript.PlaySound("playerHit");
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
                
        }

        
    }

}
