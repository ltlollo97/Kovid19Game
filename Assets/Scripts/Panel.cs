using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Panel : MonoBehaviour
{
    
    public void OnEnable()
    {
       //SoundManagerScript.StopBackgroundMusic(); // stops background music
    }

    private void Start()
    {
        SoundManagerScript.StopBackgroundMusic(); // stops background music
    }

    public void FreezeGame() // called via event keyframe in animation
    {

        Time.timeScale = 0f;
        
    }

    private void OnDisable()
    {
       SoundManagerScript.PlayBackgroundMusic();

        if(SceneManager.GetActiveScene().buildIndex != 3) // avoid reset time scaling in tutorial level MODIFY THE VALUE IF SCENE INDEX CHANGES!!!
            Time.timeScale = 1f; // resume the game as soon as the panel is closed
    }

}
