using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip breakingObjectSound, shitSound, healthSound;
    [SerializeField]public static AudioSource[] audioSrc;

    // keep the volume values set in the main menu settings
    public AudioMixer musicMixer, effectsMixer;
    public BarsUI musicSlider, effectsSlider;

    // Start is called before the first frame update
    void Start()
    {
        breakingObjectSound = Resources.Load<AudioClip>("breaking-bottle");
        shitSound = Resources.Load<AudioClip>("shit");
        healthSound = Resources.Load<AudioClip>("healthpickup");
        audioSrc = GetComponents<AudioSource>();
        
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
                audioSrc[0].PlayOneShot(breakingObjectSound);
                break;
            case "shit":
                audioSrc[0].PlayOneShot(shitSound);
                break;

            case "health":
                audioSrc[0].PlayOneShot(healthSound);
                break;

        }
        
    }

    public static void PlayBackgroundMusic()
    {
        audioSrc[1].Play();
    }

    public static void StopBackgroundMusic()
    {
        audioSrc[1].Stop();
    }
}
