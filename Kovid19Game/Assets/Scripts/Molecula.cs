using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Molecula : Enemy
{
    public int xDirection;
    public int yDirection;
    private Vector3 startingPosition;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        enemySpeed = 0.5f;
        health = 200;
        xDirection = RandomExcept(-1, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    public override void EnemyMove()
    {
        transform.position += -transform.right * enemySpeed* Time.deltaTime;
    }

}
