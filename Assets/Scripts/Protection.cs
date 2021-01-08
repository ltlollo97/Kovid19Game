using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Protection : MonoBehaviour
{
    private bool rotate = false;
    public float offsetX;
    public float offsetY;
    public float speed;
    protected GameObject boss;
    private Quaternion target;

    // Start is called before the first frame update
    void Start()
    {
        boss = GameObject.Find("MissRona");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3 (boss.transform.position.x + offsetX, boss.transform.position.y + offsetY, 0);

        if (transform.localScale.x < 1.3 && transform.localScale.y < 1.3 && transform.localScale.z < 1.3)
            Scale();

        transform.Rotate(0.0f, 0.0f, 1f);
    }

    protected void Scale()
    {
        Vector3 localScale = transform.localScale;
        localScale.x += 0.05f;
        localScale.y += 0.05f;
        localScale.z += 0.05f;
        transform.localScale = localScale;
    }
}
