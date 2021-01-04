using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystemBoss : MonoBehaviour
{

    public float score; //each level has a maximum amount of point obtainable
    public Text timerUI; //timer text on canvas
    public Text scoreText; // score text on level complete panel
    public BarsUI bossBar;
    private int threshold;
    private float percentage;
    private List<int> thresholds = new List<int>();
    private EnemyTracker tracker;
    private GameObject levelCompletePanel;
    private GameObject[] spawners;
    private bool gameEnded = false;
    private float timeSpent; //how much time the player spent in completing the level


    // Start is called before the first frame update
    void Start()
    {
        levelCompletePanel = GameObject.Find("LevelCompletePanel");
        levelCompletePanel.SetActive(false);

        timeSpent = 0f; //time when the level starts
        //    threshold = 0;

        //    completionBar.SetMaxValue(100);
        //   completionBar.SetValue(100);
    }

    // Update is called once per frame
    void Update()
    {
        timeSpent += Time.deltaTime; // time spent in seconds

        if (bossBar.GetValue() <= 0 && !gameEnded) //level completed
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
        levelCompletePanel.SetActive(true);
        score -= 2 * timeSpent;
        if (score <= 200)
            score = 200; // minimum amount of points
        int visualScore = (int)score; //type cast
        scoreText.text = visualScore.ToString();
    }
}
