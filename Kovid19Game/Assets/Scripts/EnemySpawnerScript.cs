using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    public GameObject enemy;
    float randX;
    float offset;
    int cont = 0;
    public int threshold = 5;
    Vector2 whereToSpawn;
    public float spawnRate = 2f;
    float nextSpawn = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cont < threshold)
        {
            if (Time.time > nextSpawn)
            {
                nextSpawn = Time.time + spawnRate;
                randX = Random.Range(-22f, 70f);
                whereToSpawn = new Vector2(randX, transform.position.y);
                Instantiate(enemy, whereToSpawn, Quaternion.identity);
                cont++;
                Debug.Log(cont);
            }
        }
    }
}
