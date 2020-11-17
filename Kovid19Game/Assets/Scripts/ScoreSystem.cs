using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    GameObject[] enemies;
    public int score;
    public float timeSpent; //how much time the player spent in completing the level

    // Start is called before the first frame update
    void Start()
    {
        //Initialize timeSpent
        enemies = GameObject.FindGameObjectsWithTag("Enemy"); //arrays of objects with tag Enemy
    }

    // Update is called once per frame
    void Update()
    {
        //Increase timeSpent
        //IF enemies.Length == 0 THEN level is completed
        //  the lower timeSpent, the higher score


        //Debug.Log(enemies.Length);  //outputs in console how many object with tag Enemy are in the scene  
    }

}
