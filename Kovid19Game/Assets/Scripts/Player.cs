using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int playerSpeed = 10;
    public int jumpPower = 1250;

    private bool facingLeft = false;
    private float moveX;

    // Start is called before the first frame update
    void Start()
    {
        
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
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
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
    }

    void FlipPlayer()
    {
        facingLeft = !facingLeft;
        Vector2 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
