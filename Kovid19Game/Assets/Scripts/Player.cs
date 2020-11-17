using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int playerSpeed = 10;
    public int jumpPower = 1250;
    public GameObject sanitizerPuffPrefab;

    //player status
    private bool facingLeft = false; //player orientation
    private float moveX; //horizontal movement
    private bool isGrounded = true; //if true player is not jumping 
    private int health;

    // Start is called before the first frame update
    void Start()
    {
        health = 250; 
    }


    // Update is called once per frame
    void Update()
    {
        PlayerMove();
    }

    void PlayerMove()
    {
        //controls
        moveX = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded == true)  //up arrow to jump IF the player has not jumped already
        {
            Jump();
        }

        if(Input.GetKeyDown(KeyCode.Space) == true) //player hits space bar to attack
        {
            Attack();
        }
        //animations
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

    void Jump()
    {
       //jumping code
       GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpPower);
       isGrounded = false;  
    }

    void FlipPlayer()
    {
        facingLeft = !facingLeft;
        Vector2 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    void Attack()
    {
        GameObject attack = Instantiate(sanitizerPuffPrefab, 
                                transform.position + new Vector3(1.5f,0,0), 
                                Quaternion.identity) as GameObject;
        attack.GetComponent<Rigidbody2D>().velocity = new Vector2(4f, 0);
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
            health += 100;
        }

    }
}
