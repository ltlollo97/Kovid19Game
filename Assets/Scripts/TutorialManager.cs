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
           
            if (Input.anyKeyDown) // movement tutorial
            {
                popUps[popUpIndex].SetActive(false);
                popUpIndex++;

            }
        }

        else if (popUpIndex == 1) // jump tutorial
        {
            popUps[popUpIndex].SetActive(true);
            if (Input.anyKeyDown)
            {
                popUps[popUpIndex].SetActive(false);
                popUpIndex++;
            }
        }

        else if (popUpIndex == 2) // attack tutorial
        {
            popUps[popUpIndex].SetActive(true);
            if (Input.anyKeyDown)
            {
                popUps[popUpIndex].SetActive(false);
                popUpIndex++;
            }
        }

        else if (popUpIndex == 3) // final screen tutorial 
        {
            popUps[popUpIndex].SetActive(true);
            if (Input.anyKeyDown)
            {
                popUps[popUpIndex].SetActive(false);
                firstGame = false;
                Time.timeScale = 1f;
            }
        }
        


    }
}

