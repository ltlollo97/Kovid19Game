using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject settingsMenuUI;
    public GameObject mainMenuUI;
    //Closes game whenever clicked
    public void ExitGame()
    {
        Application.Quit();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    public void Settings()
    {
        settingsMenuUI.SetActive(true);
        mainMenuUI.SetActive(false);
    }

    public void SetVolume(float value)
    {
        //set volume
    }

    public void Back()
    {
        settingsMenuUI.SetActive(false);
        mainMenuUI.SetActive(true);

    }

    public void Rules()
    {

    }
}