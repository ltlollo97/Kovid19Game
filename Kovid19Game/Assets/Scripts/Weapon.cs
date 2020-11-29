using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] GameObject bone;
    
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.parent = bone.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
