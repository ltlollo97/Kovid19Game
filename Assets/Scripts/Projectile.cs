using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public bool cameraShakingEnabled;

    public GameObject destroyEffect;
    private GameObject cam;
    
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        Invoke("DestroyProjectile", lifeTime);
    }

    // Update is called once per frame
    void DestroyProjectile()
    {
        Instantiate(destroyEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            DestroyProjectile();
            if (cameraShakingEnabled)
            {
                cam.GetComponent<CameraSystem>().enabled = false; // to allow the screen shaking
                cam.GetComponent<Animator>().SetTrigger("shake");
                cam.GetComponent<CameraSystem>().enabled = true;
            }
                
        }
            
    }
}
