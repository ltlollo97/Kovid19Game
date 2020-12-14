using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip breakingObjectSound;
    static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        breakingObjectSound = Resources.Load<AudioClip>("breaking-bottle");
        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public static void PlaySound (string clip) { 

        switch (clip)
        {
            case "breakingObject":
                audioSrc.PlayOneShot(breakingObjectSound);
                break;
        }
        
    }
}
