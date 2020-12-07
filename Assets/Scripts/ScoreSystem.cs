using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    
    public float score; //each level has a maximum amount of point obtainable
    public Text timerUI; //timer on canvas
    public Text completionState;
    public BarsUI completionBar;

    private int threshold;
    private float percentage;
    private List<int> thresholds = new List<int>();
    private EnemyTracker tracker;
    private GameObject[] spawners;
    private bool gameEnded = false;
    private GameObject[] enemies;
    private float timeSpent; //how much time the player spent in completing the level


    // Start is called before the first frame update
    void Start()
    {
        timeSpent = 0f; //time when the level starts
        threshold = 0;
        percentage = 0f;

        tracker = gameObject.GetComponent<EnemyTracker>();

        spawners = GameObject.FindGameObjectsWithTag("Spawner");

        foreach(GameObject spawner in spawners)
        {
            thresholds.Add(spawner.gameObject.GetComponent<EnemySpawnerScript>().GetThreshold());
        }

        foreach(int val in thresholds)
        {
            threshold += val;
        }
        Debug.Log("Threshold : " + threshold);
        completionBar.SetMaxValue(100);
        completionBar.SetValue(0);
    }

    // Update is called once per frame
    void Update()
    {
        timeSpent += Time.deltaTime; // time spent in seconds

        percentage = (tracker.GetDeaths()/ (float)threshold) *100; //float division to avoid rounding by zero the result

        Debug.Log("Value Captured: " + tracker.GetDeaths());
        Debug.Log("percentage:" + percentage);

        completionBar.SetValue((int)percentage);
        completionState.text = percentage.ToString() + "%";


        if (tracker.GetDeaths() == threshold && !gameEnded) //level completed
        {
            gameEnded = true;
            Win();
        }
       

        int min = Mathf.FloorToInt(timeSpent / 60);
        int sec = Mathf.FloorToInt(timeSpent % 60);
        timerUI.text = min.ToString("00") + ":" + sec.ToString("00");

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
