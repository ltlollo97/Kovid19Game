using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Droplet : Enemy
{
    public int xDirection;
    public int yDirection;
    private Vector3 startingPosition;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        enemySpeed = 1;
        health = 100;
        yDirection = RandomExcept(-1,1,0);
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();

    }

    public override void EnemyMove()
    {
        transform.position = new Vector3(moveX,moveY,0f) + Vector3.up * Mathf.Sin(Time.realtimeSinceStartup) * yDirection;
    }

}
