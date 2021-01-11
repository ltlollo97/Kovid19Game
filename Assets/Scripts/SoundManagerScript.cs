using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip breakingObjectSound, shitSound, healthSound, puffSound; // appearanceSound;
    public static AudioSource audioSrc;

    // keep the volume values set in the main menu settings
    public AudioMixer musicMixer, effectsMixer;
    public BarsUI musicSlider, effectsSlider;

    

    // Start is called before the first frame update
    void Start()
    {
        breakingObjectSound = Resources.Load<AudioClip>("breaking-bottle");
        shitSound = Resources.Load<AudioClip>("shit");
        healthSound = Resources.Load<AudioClip>("healthpickup");
        puffSound = Resources.Load<AudioClip>("poof");
        //appearanceSound = Resources.Load<AudioClip>("appearance");
        audioSrc = GetComponent<AudioSource>();

        // retrieve mixers attribs value set in main menu
        musicMixer.SetFloat("volume", PlayerPrefs.GetFloat("musicVolume"));
        musicSlider.SetFloatValue(PlayerPrefs.GetFloat("musicVolume"));
        effectsMixer.SetFloat("volume", PlayerPrefs.GetFloat("effectsVolume"));
        effectsSlider.SetFloatValue(PlayerPrefs.GetFloat("effectsVolume"));
 
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
            case "shit":
                audioSrc.PlayOneShot(shitSound);
                break;
            case "health":
                audioSrc.PlayOneShot(healthSound);
                break;
            case "puff":
                audioSrc.PlayOneShot(puffSound);
                break;
        /*    case "appear":
                audioSrc.PlayOneShot(appearanceSound);
                break;*/
        }
        
    }

    public void PlayBackgroundMusic()
    {
        //backgroundSrc.Play();
    }

    public void StopBackgroundMusic()
    {
        //backgroundSrc.Stop();
    }
}
