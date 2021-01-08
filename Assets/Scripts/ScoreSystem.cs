using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{

    public float score; //each level has a maximum amount of point obtainable
    public Text timerUI; //timer text on canvas
    public Text completionState; // completion text on canvas
    public Text scoreText; // score text on level complete panel
    public BarsUI completionBar; // completion bar to fill on canvas

    private int threshold;
    private float percentage;
    private List<int> thresholds = new List<int>(); //max number of enemies spawned by each spawnerr
    private EnemyTracker tracker;
    private GameObject levelCompetePanel;
    private GameObject[] spawners;
    private bool gameEnded = false;
    private float timeSpent; //how much time the player spent in completing the level


    // Start is called before the first frame update
    void Start()
    {
        levelCompetePanel = GameObject.Find("LevelCompletePanel");
        levelCompetePanel.SetActive(false);

        timeSpent = 0f; //time when the level starts
        threshold = 0;
        percentage = 0f;

        tracker = gameObject.GetComponent<EnemyTracker>();

        spawners = GameObject.FindGameObjectsWithTag("Spawner");

        foreach (GameObject spawner in spawners)
        {
            thresholds.Add(spawner.gameObject.GetComponent<WaveSpawner>().EnemyNumber());
        }

        foreach (int val in thresholds)
        {
            threshold += val; // total number of enemies in level
        }
        Debug.Log("Threshold : " + threshold);
        completionBar.SetMaxValue(100);
        completionBar.SetValue(0);
    }

    // Update is called once per frame
    void Update()
    {
        timeSpent += Time.deltaTime; // time spent in seconds

        percentage = (tracker.GetDeaths() / (float)threshold) * 100; //float division to avoid rounding by zero the result
        int progress = (int)percentage;
        completionBar.SetValue(progress);
        completionState.text = progress.ToString() + "%";


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
        levelCompetePanel.SetActive(true);
        score -= 2 * timeSpent;
        if (score <= 200)
            score = 200; // minimum amount of points
        int visualScore = (int)score; //type cast
        scoreText.text = visualScore.ToString();

        // update points 
        int currentPoints = PlayerPrefs.GetInt("totalPoints");
        currentPoints += visualScore; // calculate new amount
        PlayerPrefs.SetInt("totalPoints", currentPoints);
    }

}
