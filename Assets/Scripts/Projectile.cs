using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public int attackValue;
    public bool cameraShakingEnabled;
    public bool isUltra;

    public GameObject destroyEffect;
    private GameObject cam;
    
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Camera Obj");
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
            if(!isUltra)
                DestroyProjectile(); // normal attack destroys as soon as hits an enemy

            if (cameraShakingEnabled)
            {
                cam.GetComponentInChildren<Animator>().SetTrigger("shake");
            }
                
        }



            
    }
}
