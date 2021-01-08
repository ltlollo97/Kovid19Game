using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Submarine : MonoBehaviour
{
    public float startPoint;
    private float startAngle = 0;
    public float endPoint;
    private float temp;
    public float speed;
    public bool needToFlip = false;
    public float angle;
    private Vector3 position;
    private Quaternion target;
    private bool rotate = false;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(startPoint, transform.position.y, 0);
        position = new Vector3(endPoint, transform.position.y, 0);
        target = Quaternion.Euler(0, 0, angle);
    }

    // Update is called once per frame
    void Update()
    {
        // TRANSLATE
        Translate(ref position);
        // ROTATE
        Rotate();
    }

    protected void Translate(ref Vector3 position)
    {
        if (Mathf.Abs(transform.position.x - position.x) > 0.2)
            transform.position = Vector2.MoveTowards(transform.position, position, speed * Time.deltaTime);

        else
        {
            if (needToFlip)
                Flip();
            Swap(ref startPoint, ref endPoint);
            position = new Vector3(endPoint, transform.position.y, 0);
        }
    }

    protected void Rotate()
    {
        if (!rotate)
        {
            Quaternion target = Quaternion.Euler(0, 0, angle);

            if (transform.rotation != target)
                transform.rotation = Quaternion.Slerp(transform.rotation, target, speed * Time.deltaTime);
            else
                rotate = true;
        }

        else
        {
            Quaternion target = Quaternion.Euler(0, 0, 0);
            if (transform.rotation != target)
                transform.rotation = Quaternion.Slerp(transform.rotation, target, speed * Time.deltaTime);
            else
                rotate = false;
        }
    }

    protected void Flip()
    {
        Vector2 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    protected void Swap(ref float startPoint, ref float endPoint)
    {
        float temp = startPoint;
        startPoint = endPoint;
        endPoint = temp;
    }
}