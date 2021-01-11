using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalPlatform : MonoBehaviour
{
    private PlatformEffector2D effector;
    public float waitTime;
    public Joystick joystick;

    // Start is called before the first frame update
    void Start()
    { 
        effector = GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S) || (joystick.Vertical < 0f && joystick.Vertical > -.5f))
        {
            waitTime = 0.1f;
        }

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S) || joystick.Vertical <= -.5f)
        {
           
            if(waitTime <= 0)
            {
                Debug.Log("I'm here");
                effector.rotationalOffset = 180f;
                waitTime = 0.1f;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W) || joystick.Vertical >= .5f)
        {
            effector.rotationalOffset = 0f;
        }
    }
}
