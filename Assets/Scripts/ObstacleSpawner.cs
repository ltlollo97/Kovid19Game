using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstacle1;
    public GameObject obstacle2;
    public GameObject obstacle3;
    public float spawnRate = 2f;
    public int threshold = 5;

    private GameObject cam;
    private Vector2 whereToSpawn;
    private float randX;
    private int randObject;
    private float offset;
    private int cont = 0;
    private float nextSpawn = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {

        if (cont < threshold)
        {

            if (Time.time > nextSpawn)
            {
                nextSpawn = Time.time + spawnRate;
                randX = Random.Range(-12f, 10f);
                randObject = Random.Range(1, 3);
                whereToSpawn = new Vector2(transform.position.x, transform.position.y);
                if (randObject == 1)
                Instantiate(obstacle1, whereToSpawn, Quaternion.identity);
                else if (randObject == 2)
                Instantiate(obstacle2, whereToSpawn, Quaternion.identity);
                else
                Instantiate(obstacle3, whereToSpawn, Quaternion.identity);
                cont++;
                //Debug.Log(cont);
            }
        }

    }

    public int GetThreshold()
    {
        return threshold;
    }
}
