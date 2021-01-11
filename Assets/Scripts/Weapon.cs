using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Weapon : MonoBehaviour
{

    [Header("Uncheck if firing mechanism should be disabled")]
    [SerializeField]private bool canFire = true; 

    private float startTimeBetweenShots;
    public SpriteRenderer sanitizer;
    public Transform shotPoint;
    public AudioSource shotSound, ultraSound;
    public BarsUI superAttackBar;
    public bool mobile;

    public GameObject[] normalAttackPrefab;
    public GameObject[] ultraAttackPrefab;
    
    public List<Sprite> sprites = new List<Sprite>();

    // 0 : BLUE SANITIZER
    // 1 : GREEN SANITIZER
    // 2 : RED SANITIZER

    private float timeBtwShots;
    private bool ultraReady;
    private float timePassed;
    //[Header("N.B. is a private variable")]
    public float ultimateAttackCooldown = 60f;

    [SerializeField] private Transform player;
    void Start()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            mobile = true;
        }
    }

    public void SelectSanitizer(int index)
    {
        sanitizer.sprite = sprites[index]; // change mask sprite
        PlayerPrefs.SetInt("sanitizerEquipped", index);
        UpdateFiringRate(); // updates startTimeBetweenShots
        Debug.Log("Sanitizer equipped: " + PlayerPrefs.GetInt("sanitizerEquipped"));
    }

    private void UpdateFiringRate()
    {
        if(PlayerPrefs.GetInt("sanitizerEquipped") == 0)
        {
            startTimeBetweenShots = 0.6f;
        }
        else if (PlayerPrefs.GetInt("sanitizerEquipped") == 1)
        {
            startTimeBetweenShots = 1.1f;
        }
        else if (PlayerPrefs.GetInt("sanitizerEquipped") == 2)
        {
            startTimeBetweenShots = 0.8f;
        }
    }

    private void Update()
    {
        if (canFire)
            Fire();
    }
    

    private void Fire()
    {
        if (timePassed >= ultimateAttackCooldown && !ultraReady) //player can cast ultimate attack
        {
            ultraReady = true;
            timePassed = 0f;
            StopCoroutine(ChargeSuperBar());
        }
        else if (!ultraReady)
        {
            timePassed += Time.deltaTime;
            StartCoroutine(ChargeSuperBar());
        }


        if (timeBtwShots <= 0)
        {
            if ((!mobile && Input.GetMouseButton(0))/*||(mobile  &&) */) // left click or clicking on the right of the screen
            {
                if (!shotSound.isPlaying)
                    shotSound.Play();
                Instantiate(normalAttackPrefab[PlayerPrefs.GetInt("sanitizerEquipped")], shotPoint.position, shotPoint.rotation);
                timeBtwShots = startTimeBetweenShots;
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }

        if (ultraReady)
        {
            if (Input.GetMouseButton(1)/*||(mobile  &&) */) // right click
            {
                if (!ultraSound.isPlaying)
                    ultraSound.Play();
                Instantiate(ultraAttackPrefab[PlayerPrefs.GetInt("sanitizerEquipped")], shotPoint.position, shotPoint.rotation);
                ultraReady = false;
                superAttackBar.SetFloatValue(0f); // resets super attack bar
            }
        }
    }

    private IEnumerator ChargeSuperBar()
    {
        superAttackBar.SetFloatValue(timePassed);
        yield return new WaitForSeconds(1f);
    }
}
