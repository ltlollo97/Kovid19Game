using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public float distance;
    public int attackValue;
    public bool cameraShakingEnabled;
    public bool isDestructible;

    public GameObject destroyEffect;
    private GameObject cam;
    private float dir;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Camera Obj");
        Invoke("DestroyProjectile", lifeTime);
        if (cameraShakingEnabled)
        {
            cam.GetComponentInChildren<Animator>().SetTrigger("shake");
        }
    }

    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
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
           if(isDestructible)
                DestroyProjectile(); // normal attack destroys as soon as hits an enemy
       }
     
   }
}
