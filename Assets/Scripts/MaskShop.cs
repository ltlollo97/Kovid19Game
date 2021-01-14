using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaskShop : MonoBehaviour
{
    public Button[] options;
    public GameObject[] soldOut;
    public Sprite sold;
    public Text totalPoints;

    private int maskSold;

    // Start is called before the first frame update
    void Start()
    {
        // UNCOMMENT FOR DEBUG
        //if (Application.isEditor)
        //{
           // PlayerPrefs.SetInt("totalPoints", 3000);
           // PlayerPrefs.SetInt("maskSold", 0); // 0: false, 1: true
        //}

        maskSold = PlayerPrefs.GetInt("maskSold");
    }

    // Update is called once per frame
    void Update()
    {
        totalPoints.text = PlayerPrefs.GetInt("totalPoints").ToString();

        if (maskSold == 0)
        {
            if (PlayerPrefs.GetInt("totalPoints") >= 2000) // player can buy the item
            {
                options[1].interactable = true;
                soldOut[0].SetActive(false); // sold out image not displayed
            }
            else // player cannot buy the item
            {
                options[1].interactable = false;
                soldOut[0].SetActive(true); // sold out image displayed
            }
        }
        else
        {
            soldOut[1].GetComponent<Image>().sprite = sold;
            soldOut[1].SetActive(true);
        }
    }

    public void BuyItem(int index) // updates current amount of points
    {
        int amount = PlayerPrefs.GetInt("totalPoints");

        if (index == 1 && maskSold == 0) // player bought KN95 mask 
        {
            amount -= 2000;
            soldOut[index-1].GetComponent<Image>().sprite = sold;
            maskSold = 1;
            soldOut[index-1].SetActive(true);
            PlayerPrefs.SetInt("totalPoints", amount);
        }

    }
}
