using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bola : MonoBehaviour
{
    private AudioSource myAudioSource;
    

    private void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Spada")
        {
            Destroy(this.gameObject);
            Debug.Log("Destroyed");
        }
    }
}
