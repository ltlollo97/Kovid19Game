using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] popUps;
    public GameObject[] popUpsMobile;
    private int popUpIndex = 0;
    private bool firstGame = true;
    private bool mobile = false;


    private void Start()
    {
        Time.timeScale = 0f;
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            mobile = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!firstGame)
        {
            //Debug.Log("Tutorial Ended");
            return;
        }

        if (mobile)
        {
            mobile_version();
        }else
        {
            default_version();
        }
    }

    void default_version()
    {

            if (popUpIndex == 0)
            {
                popUps[popUpIndex].SetActive(true);
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

    void mobile_version()
    {

            popUpsMobile[popUpIndex].SetActive(true);
            if (popUpIndex == 0)
                {

                if (Input.anyKeyDown) // movement tutorial
                {
                    popUpsMobile[popUpIndex].SetActive(false);
                    popUpIndex++;

                }
            }

            else if (popUpIndex == 1) // jump tutorial
            {
                popUpsMobile[popUpIndex].SetActive(true);
                if (Input.anyKeyDown)
                {
                    popUpsMobile[popUpIndex].SetActive(false);
                    popUpIndex++;
                }
            }

            else if (popUpIndex == 2) // attack tutorial
            {
                popUpsMobile[popUpIndex].SetActive(true);
                if (Input.anyKeyDown)
                {
                    popUpsMobile[popUpIndex].SetActive(false);
                    popUpIndex++;
                }
            }

            else if (popUpIndex == 3) // final screen tutorial 
            {
                popUpsMobile[popUpIndex].SetActive(true);
                if (Input.anyKeyDown)
                {
                    popUpsMobile[popUpIndex].SetActive(false);
                    firstGame = false;
                    Time.timeScale = 1f;
                }
            }
        }


}

