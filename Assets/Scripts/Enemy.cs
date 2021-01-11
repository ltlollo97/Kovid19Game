using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected float moveX;
    protected float moveY;

    public AudioSource hitSound;
    public float enemySpeed;
    public int health;
    public GameObject smoke;
    protected bool facingLeft = false;
    protected float minDistance = 0.2f;
    protected Player player;
    protected float playerUltraCooldown;
    protected Animator anim;
    private bool addDeath = false;

    // Start is called before the first frame update
    public void Start()
    {
        Instantiate(smoke, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        anim = GetComponent<Animator>();
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }

    // Update is called once per frame
    public void Update()
    {
        ChasePlayer();

        if (health <= 0 && !addDeath)
        {
            if(!hitSound.isPlaying)
                hitSound.Play();

            EnemyTracker tracker = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<EnemyTracker>();
            tracker.AddDeath();
            addDeath = true;

            StartCoroutine(Die());
        }
    }


    //public abstract void EnemyMove();

    protected abstract void ChasePlayer();

    public int RandomExcept(int min, int max, int except)
    {
        int random = Random.Range(min, max);
        if (random == 0) random = 1;
        return random;
    }

    protected void FlipEnemy()
    {
        facingLeft = !facingLeft;
        Vector2 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private IEnumerator Die()
    {
        anim.Play("Hit");
        yield return new WaitForSeconds(1f);
        Instantiate(smoke, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
        yield return new WaitForSeconds(1.8f);        // length of the "Die" animation: 0.667 (+ approx. time for the sound to end)
        Destroy(gameObject);
    }

}
