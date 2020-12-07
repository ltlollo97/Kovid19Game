using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    public GameObject enemy;
    public float spawnRate = 2f;
    public int threshold = 5;

    private GameObject cam;
    private Vector2 whereToSpawn;
    //private float randX;
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
        transform.position = new Vector2(cam.transform.position.x + 12f, transform.position.y); // enemy can't spawn 
        if (cont < threshold)
        {
            
            if (Time.time > nextSpawn)
            {
                nextSpawn = Time.time + spawnRate;
                //randX = Random.Range(-22f, 70f);
                whereToSpawn = new Vector2(transform.position.x, transform.position.y);
                Instantiate(enemy, whereToSpawn, Quaternion.identity);
                cont++;
                Debug.Log(cont);
            }
        }
       
    }
    
    public int GetThreshold()
    {
        return threshold;
    }
}
