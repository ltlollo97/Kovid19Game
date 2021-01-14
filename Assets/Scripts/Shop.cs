using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{

    public Button[] options;
    public GameObject[] soldOut;
    public Sprite sold;
    public Text totalPoints;

    private int greenSold, redSold;

    // Start is called before the first frame update
    void Start()
    {
        // UNCOMMENT FOR DEBUG
        if (Application.isEditor)
        {
           PlayerPrefs.SetInt("totalPoints", 6000);
           PlayerPrefs.SetInt("greenSold", 0); // 0: false, 1: true
           PlayerPrefs.SetInt("redSold", 0); // 0: false, 1: true
        }
        else
        {
            PlayerPrefs.SetInt("totalPoints", 3000);
        }

        greenSold = PlayerPrefs.GetInt("greenSold");
        redSold = PlayerPrefs.GetInt("redSold");
    }

    // Update is called once per frame
    void Update()
    {
        totalPoints.text = PlayerPrefs.GetInt("totalPoints").ToString();

        if(greenSold == 0) // player has not bought the green sanitzer yet
        {
            if (PlayerPrefs.GetInt("totalPoints") >= 1000) // player can buy the item
            {
                options[1].interactable = true;
                soldOut[1].SetActive(false); // sold out image not displayed
                Debug.Log("You can buy the green sanitizer");
            }
            else // player cannot buy the item
            {
                options[1].interactable = false;
                soldOut[1].SetActive(true); // sold out image displayed
                Debug.Log("You can't buy the green sanitizer");
            }
        }
        else
        {
            soldOut[1].GetComponent<Image>().sprite = sold;
            soldOut[1].SetActive(true);
        }

        if(redSold == 0) // player has not bought the red sanitzer yet
        {
            if (PlayerPrefs.GetInt("totalPoints") >= 2750)
            {
                options[2].interactable = true;
                soldOut[2].SetActive(false); // sold out image not displayed
                Debug.Log("You can buy the red sanitizer");
            }
            else
            {
                options[2].interactable = false;
                soldOut[2].SetActive(true); // sold out image displayed
                Debug.Log("You can't buy the red sanitizer");
            }
        }
        else
        {
            soldOut[2].GetComponent<Image>().sprite = sold;
            soldOut[2].SetActive(true);
        }
    }

    public void BuyItem(int index) // updates current amount of points
    {
        int amount = PlayerPrefs.GetInt("totalPoints"); 

        if (index == 1 && greenSold == 0) // player bought green sanitizer
        {
            amount -= 1000;
            soldOut[1].GetComponent<Image>().sprite = sold;
            greenSold = 1;
            PlayerPrefs.SetInt("greenSold", 1);
            soldOut[1].SetActive(true);
            PlayerPrefs.SetInt("totalPoints", amount);
        }
        else if (index == 2 && redSold == 0) // player bought red sanitizer
        {
            amount -= 2750;
            soldOut[2].GetComponent<Image>().sprite = sold;
            redSold = 1;
            PlayerPrefs.SetInt("redSold", 1);
            soldOut[2].SetActive(true);
            PlayerPrefs.SetInt("totalPoints", amount);
        }
    }

}
