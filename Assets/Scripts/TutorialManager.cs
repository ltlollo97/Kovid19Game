using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] popUps;
    private int popUpIndex = 0;
    private bool firstGame = true;


    private void Start()
    {
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!firstGame)
        {
            //Debug.Log("Tutorial Ended");
            return;
        }
            
        if (popUpIndex == 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow)) // movement tutorial
            {
                popUps[popUpIndex].SetActive(false);
                popUpIndex++;

            }
        }

        else if (popUpIndex == 1) // attack tutorial
        {
            popUps[popUpIndex].SetActive(true);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                popUps[popUpIndex].SetActive(false);
                popUpIndex++;
            }
        }

        else if (popUpIndex == 2) // final screen tutorial
        {
            popUps[popUpIndex].SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                popUps[popUpIndex].SetActive(false);
                //PlayerPrefs.SetInt("firstGame", 1);
                firstGame = false;
                Time.timeScale = 1f;
            }
        }

        
    }
}

