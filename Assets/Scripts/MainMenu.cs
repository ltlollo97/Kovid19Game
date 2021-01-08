using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioMixer musicMixer;
    public AudioMixer effectsMixer;

    //Closes game whenever clicked
    public void ExitGame()
    {
        Application.Quit();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("levelAt"));
    }

    public void Load(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void SetMusicVolume(float volume)
    {
        musicMixer.SetFloat("volume", volume);
        PlayerPrefs.SetFloat("musicVolume", volume);

        Debug.Log("MUSIC EFFECTS VOLUME SET AT: " + volume);
    }

    public void SetEffectsVolume(float volume)
    {
        effectsMixer.SetFloat("volume", volume);
        PlayerPrefs.SetFloat("effectsVolume", volume);
    }
}
