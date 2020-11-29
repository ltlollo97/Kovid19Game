using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    
    public float score; //each level has a maximum amount of point obtainable
    //public Text textUI;

    private bool gameEnded = false;
    private GameObject[] enemies;
    private float timeSpent; //how much time the player spent in completing the level

    // Start is called before the first frame update
    void Start()
    {
        timeSpent = 0f; //time when the level starts
        //textUI.text = "0";
        //enemies = GameObject.FindGameObjectsWithTag("Enemy"); //arrays of objects with tag Enemy
    }

    // Update is called once per frame
    void Update()
    {
        timeSpent += Time.deltaTime; // time spent in seconds
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        

        if (enemies.Length == 0 && !gameEnded)
        {
            gameEnded = true;
            Win();

        }

        //Debug.Log(enemies.Length);  //outputs in console how many object with tag Enemy are in the scene  
    }


    public float GetTimeSpent()
    {
        return timeSpent;
    }
    
    private void Win()
    {
        //  the lower timeSpent, the higher score
        score -= 0.01f * timeSpent;
        int visualScore = (int) score; //type cast
       // textUI.text = "You achieved " + visualScore.ToString() + " points";
    }

}
