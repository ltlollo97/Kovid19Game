using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected float moveX;
    protected float moveY;
    protected float enemySpeed;
    protected int health;

    // Start is called before the first frame update
    public void Start()
    {
        //get enemy initial position
        moveX = gameObject.transform.position.x;
        moveY = gameObject.transform.position.y;
    }

    // Update is called once per frame
    public void Update()
    {
        EnemyMove();
    }


    public abstract void EnemyMove();

    public int RandomExcept(int min, int max, int except)
    {
        int random = Random.Range(min, max);
        if (random == 0) random = 1;
        return random;
    }

}
