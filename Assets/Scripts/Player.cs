using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public AudioSource jumpSound, hitSound, spraySound, ultraSound, dashSound;
    public int playerSpeed = 10;
    public int jumpPower;
    public float dashDuration;
    public float dashCooldown;
    public GameObject sanitizerPuffPrefab;
    public GameObject sanitizerUltraPrefab;
    public Transform projectileSpawnPoint;
    public BarsUI superAttackBar, healthBar;
    //public float startTimeBetweenShots;
    public float ultimateAttackCooldown;
    


    // movement
    private bool facingLeft = false; //player orientation
    private float moveX; //horizontal movement
    private bool canDash = true;
    private bool isDashing;
    private float gravityScal;
    private IEnumerator dashCoroutine;
    private float direction;
    private bool isGrounded = true; //if true player is not jumping 
    // attack
    private bool ultraReady = false; 
    private float nextUltimateFire;
    private float timeBetweenShots;
    // status
    public int baseHealth; // base HP value without bonus
    private int health; // maximum player health = base + bonus
    private bool isDead = false;
    private bool invulnerable = false;
    private float timePassed;
    private float invincibilityTime = 3f;
    // other game objs
    private Animator playerAnimator;
    private GameObject gameOverPanel;
    // equip
    private Mask mask;
    private Weapon sanitizer;
    // android 
    public Joystick joystick;
    public FixedButton attack;
    public FixedButton supAttack;
    public RuntimePlatform platform;


    // Start is called before the first frame update
    void Start()
    {
        InitializeEquip();

        
        gravityScal = gameObject.GetComponent<Rigidbody2D>().gravityScale;

        GameObject camObj = GameObject.FindGameObjectWithTag("MainCamera");

        gameOverPanel = GameObject.Find("GameOverPanel");
        gameOverPanel.SetActive(false);

        playerAnimator = gameObject.GetComponent<Animator>();
        nextUltimateFire = ultimateAttackCooldown; // wait 60 secs at the beginning

        // set initial values for UI bars
        superAttackBar.SetMaxValue((int)ultimateAttackCooldown);
        superAttackBar.SetValue(0);//when filled up completely, the playe can cast an ultra attack
        healthBar.SetMaxValue(health);

        //set the platform we use 
        platform = Application.platform;

    }


    // Update is called once per frame
    void Update()
    {
        if (!isDead)
            PlayerControl();

        if (timePassed >= ultimateAttackCooldown && !ultraReady) //player can cast ultimate attack
        {
            ultraReady = true;
            timePassed = 0f;
            supAttack.gameObject.SetActive(true);
            StopCoroutine(ChargeSuperBar());
        }
        else if (!ultraReady)
        {
            timePassed += Time.deltaTime;
            supAttack.gameObject.SetActive(false);
            StartCoroutine(ChargeSuperBar());
        }

        if (healthBar.GetValue() <= 0 && !isDead)
        {
            isDead = true;
            Die();
        }

        //updates Sliders value
        //if (!ultraReady)
           // superAttackBar.SetValue((int)Time.time % (int)(ultimateAttackCooldown) + 1);

        if (!isDead)
            healthBar.SetValue(health);
    }

   
    public float GetUltraCooldown()
    {
        return nextUltimateFire;
    }

    public void SetUltraCooldown(float val)
    {
        nextUltimateFire = val;
    }

    private void PlayerControl()
    {
        // ------------- ATTACK ----------------------
        if (timeBetweenShots <= 0)
        {
           if ((Input.GetKeyDown(KeyCode.Space)||attack.Pressed) == true) //player hits space bar to attack
            {

                playerAnimator.Play("Attack");
                Attack(sanitizerPuffPrefab, Vector2.zero); // spawn without offset
                if (!spraySound.isPlaying)
                    spraySound.Play();
                timeBetweenShots = sanitizer.startTimeBetweenShots;
            }
        }
        else
        {
            timeBetweenShots -= Time.deltaTime;
        }
        // -- ULTRA ATTACK 
        if ((Input.GetKeyDown(KeyCode.Q) || supAttack.Pressed) && ultraReady == true) //ultra attack available,  player hits 'q' to perform an ultra attack
        {
            ultraReady = false;
            superAttackBar.SetFloatValue(0f); // resets super attack bar
            //nextUltimateFire = Time.time + ultimateAttackCooldown; //reset cooldown
            Attack(sanitizerUltraPrefab, new Vector2(0.0f, 0.5f));
            //UltraAttack();
            if (!ultraSound.isPlaying)
                ultraSound.Play();
        }
        // ------------------------------------------------


        // -------------- JUMP ----------------------------
        
        // -- JUMP ANIMATION
        if (isGrounded)
        {
            playerAnimator.SetBool("isJumping", false);
        }
        // --

        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || joystick.Vertical>=.5f ) && isGrounded == true)  //up arrow (or W) to jump IF the player has not jumped already
        {
            playerAnimator.SetTrigger("takeOff");
            Jump();
            if (!jumpSound.isPlaying)
                jumpSound.Play();
        }
        // -------------------------------------------



        //--------- FLIP CHARACTER SPRITE -------------
        if (moveX < 0.0f && facingLeft == false)
        {
            FlipPlayer();
        }
        else if (moveX > 0.0f && facingLeft == true)
        {
            FlipPlayer();
        }

        // ---------------------------------------------


        // ------------ MOVEMENT ON X AXIS -------------
        
        if (platform== RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer)
        {
            moveX = joystick.Horizontal; 
        }
        else
        {
            moveX = Input.GetAxis("Horizontal");
            //TOREMOVE
            moveX = joystick.Horizontal;
        }
        
        if (moveX != 0)
            direction = moveX;

        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * playerSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);

        // -- RUNNING ANIMATION
        if (moveX == 0) //player is not moving
        {
            playerAnimator.SetBool("isRunning", false);
        }
        else
        {
            playerAnimator.SetBool("isRunning", true);
        }
        // --------------------------------------------


        // ------------ DASH --------------------------
        if ((Input.GetKeyDown(KeyCode.LeftShift)|| joystick.Horizontal >= .8f || joystick.Horizontal <= -.8f) && canDash == true)
        {
            if (dashCoroutine != null)
                StopCoroutine(dashCoroutine);
            dashCoroutine = Dash(.3f, 3f);
            StartCoroutine(dashCoroutine);
            if (!dashSound.isPlaying)
                dashSound.Play();
        }

        if (isDashing)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(direction * 20, 0), ForceMode2D.Impulse);
        }
    }


    private void Jump()
    {
        isGrounded = false;
        playerAnimator.SetBool("isJumping", true);
        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0f);
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpPower, ForceMode2D.Force);
    }

    private void FlipPlayer()
    {
        facingLeft = !facingLeft;
        Vector2 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void Attack(GameObject projectilePrefab, Vector3 offset) //instantiate projectilePrefab attack with an offset
    {

        GameObject attack = Instantiate(projectilePrefab, projectileSpawnPoint.position + offset, projectileSpawnPoint.rotation);

        float speed = projectilePrefab.GetComponent<Projectile>().speed;

        if (facingLeft)
        {
            Vector2 localScale = attack.transform.localScale;
            localScale.x *= -1;
            attack.transform.localScale = localScale;
            attack.GetComponent<Rigidbody2D>().velocity = new Vector2(speed * -projectileSpawnPoint.right.x, 0);
        }
        else
        {
            attack.GetComponent<Rigidbody2D>().velocity = new Vector2(speed * projectileSpawnPoint.right.x, 0);
        }


    }

    private void InitializeEquip()
    {
        mask = GetComponentInChildren<Mask>();
        mask.SelectOption(PlayerPrefs.GetInt("maskEquipped"));
        baseHealth += mask.bonusHP; // health = base + mask power up
        health = baseHealth; // at the beginning, current health is max
        Debug.Log("Player hp: " + health);

        sanitizer = GetComponentInChildren<Weapon>();
        sanitizer.SelectSanitizer(PlayerPrefs.GetInt("sanitizerEquipped"));
        sanitizerPuffPrefab = GetComponentInChildren<Weapon>().normalAttackPrefab[PlayerPrefs.GetInt("sanitizerEquipped")]; // update projectile sprite
        sanitizerUltraPrefab = GetComponentInChildren<Weapon>().ultraAttackPrefab[PlayerPrefs.GetInt("sanitizerEquipped")]; // update ultra proj prefab
        Debug.Log("SANITIZER : " + PlayerPrefs.GetInt("sanitizerEquipped"));
    }

    private void Die()
    {
        playerAnimator.Play("Die"); //play Die animation
        Destroy(gameObject, 2f); // wait 2 secs then destroy the game object
        Debug.Log("Player is Dead");
        gameOverPanel.SetActive(true); //load Defeat Scene
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Player has collided with " + collision.collider.name);

        if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "Platform")
        {

            isGrounded = true; 
        }

        if (!invulnerable)
        {
            if ((collision.gameObject.tag == "Enemy" && !isGrounded) || collision.gameObject.tag == "Enemy")
            {
                playerAnimator.Play("Hit");
                if (!hitSound.isPlaying)
                {
                    hitSound.Play();
                }
                StartCoroutine(Invulnerability(collision));
                health -= 20;

            }

            if (collision.gameObject.tag == "HealthKit")
            {
                if (health + 100 < baseHealth + mask.bonusHP)
                {
                    health += 100;
                }
                else
                {
                    health = baseHealth + mask.bonusHP;
                }
            }

            if (collision.gameObject.tag == "Object")
            {
                playerAnimator.Play("Hit");
                if (!hitSound.isPlaying)
                    hitSound.Play();
                StartCoroutine(Invulnerability(collision));
                health -= 10;
            }
        }
    }

    private IEnumerator Invulnerability(Collision2D collider)
    {
        invulnerable = true;
        gameObject.layer = 10; // switch to Immune layer
        StartCoroutine(DamageAnimation()); // lasts 1.5 secs
        yield return new WaitForSeconds(invincibilityTime);
        gameObject.layer = 8; // bring back to PlayerGenerated layer
        invulnerable = false; // no longer invulnerable
    }

    private IEnumerator DamageAnimation() // character sprites flash for little time
    {
        SpriteRenderer[] srs = GetComponentsInChildren<SpriteRenderer>();

        for (int i = 0; i < 15; i++)
        {
            foreach (SpriteRenderer sr in srs)
            {
                Color c = sr.color;
                c.a = 0.5f;
                sr.color = c;
            }

            yield return new WaitForSeconds(.1f);

            foreach (SpriteRenderer sr in srs)
            {
                Color c = sr.color;
                c.a = 1;
                sr.color = c;
            }

            yield return new WaitForSeconds(.1f);
        }
    }

    private IEnumerator Dash(float dashDuration, float dashCooldown)
    {
        isDashing = true;
        canDash = false;
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 0f;
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        gameObject.GetComponent<Rigidbody2D>().gravityScale = gravityScal;
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    private IEnumerator ChargeSuperBar()
    {
        superAttackBar.SetFloatValue(timePassed);
        yield return new WaitForSeconds(1f);
    }


}


