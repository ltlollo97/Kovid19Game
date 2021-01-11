using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveProjectilePoint : MonoBehaviour
{
    public Transform player;
    private bool isFlipped;
    // Update is called once per frame
    void Update()
    {
        if (player.localScale.x < 0 && !isFlipped)
        {
            isFlipped = true;
            transform.Rotate(0f, 180f, 0f);
        }
        else if (player.localScale.x > 0 && isFlipped)
        {
            isFlipped = false;
            transform.Rotate(0f, 180f, 0f);
        }
    }
}
