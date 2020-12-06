using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip playerHitSound, jumpSound, enemy1HitSound, enemy2HitSound, ronaHelloSound, ronaHitSound;
    static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        playerHitSound = Resources.Load<AudioClip>("player-scream");
        jumpSound = Resources.Load<AudioClip>("player-jump");
        enemy1HitSound = Resources.Load<AudioClip>("enemy-hit");
        enemy2HitSound = Resources.Load<AudioClip>("enemy2-hit");
        ronaHelloSound = Resources.Load<AudioClip>("rona-scream");
        ronaHitSound = Resources.Load<AudioClip>("rona-dies");

        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public static void PlaySound (string clip) { 

        switch (clip)
        {
            case "playerHit":
                audioSrc.PlayOneShot(playerHitSound);
                break;
            case "jump":
                audioSrc.PlayOneShot(jumpSound);
                break;
            case "enemy1Hit":
                audioSrc.PlayOneShot(enemy1HitSound);
                break;
            case "enemy2Hit":
                audioSrc.PlayOneShot(enemy2HitSound);
                break;
            case "ronahello":
                audioSrc.PlayOneShot(ronaHelloSound);
                break;
            case "ronahit":
                audioSrc.PlayOneShot(ronaHitSound);
                break;
        }
        
    }
}
