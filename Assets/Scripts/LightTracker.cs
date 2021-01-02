using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTracker : MonoBehaviour
{
    [SerializeField]
    GameObject character;
    [SerializeField]
    Vector2 posOffset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(character.transform.position.x + posOffset.x, character.transform.position.y + posOffset.y, character.transform.position.z);
    }
}
