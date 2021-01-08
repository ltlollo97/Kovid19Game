using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shit : MonoBehaviour
{
    public AudioSource fallingSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        bool touched = false;

        if (col.gameObject.name.Equals("Player"))
            Debug.Log("Got you!");
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Floor")
        {
            if (!touched)
            {
                SoundManagerScript.PlaySound("shit");
                touched = true;
            }

            Destroy(gameObject);
        }
    }
}
