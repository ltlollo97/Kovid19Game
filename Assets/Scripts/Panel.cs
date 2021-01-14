using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Panel : MonoBehaviour
{
    public AudioSource backgroundSrc;

    public void OnEnable()
    {
        backgroundSrc.mute = true; // stops background music
    }


    public void FreezeGame() // called via event keyframe in animation
    {

        Time.timeScale = 0f;
        
    }

    private void OnDisable()
    {
        if (backgroundSrc != null)
            backgroundSrc.mute = false; // play background music again

        if (SceneManager.GetActiveScene().buildIndex != 2) // avoid reset time scaling in tutorial level MODIFY THE VALUE IF SCENE INDEX CHANGES!!!
            Time.timeScale = 1f; // resume the game as soon as the panel is closed
    }

}
