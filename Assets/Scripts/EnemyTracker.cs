using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTracker : MonoBehaviour
{

    public int deadEnemies;

    private void Start()
    {
        deadEnemies = 0;
    }

    public void AddDeath()
    {
        deadEnemies++;
        Debug.Log("Tracker: " + deadEnemies);
    }

    public int GetDeaths()
    {
        return deadEnemies;
    }

}
