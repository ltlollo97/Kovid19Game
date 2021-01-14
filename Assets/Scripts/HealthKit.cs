using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthKit : MonoBehaviour
{
    public GameObject healthkit; // prefab of health kit object
    public Transform player; // position of the player
    public BarsUI playerBar;
    public int occurrences; // how many kit to spawn

    private int count = 0;
    
    private int res;
    private bool go = true;
    private bool firstUpdate = false;
    private float maxLife;
    private int factor = 2;
    private float[] distances;
    private int lastSpawnPoint;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (playerBar.GetValue() <= (maxLife/factor) && count < occurrences && go)
        {
            Instantiate(healthkit, new Vector2(player.position.x, 8.15f), Quaternion.identity);
            count++;
            factor++;
            StartCoroutine(Wait());
        }
    }

    private void FixedUpdate()
    {
        if (!firstUpdate)  // update only once
        {
            maxLife = playerBar.GetValue();
            firstUpdate = true;
        }

    }

    private IEnumerator Wait()
    {
        go = false;
        yield return new WaitForSeconds(10f);
        go = true;
    }

}
