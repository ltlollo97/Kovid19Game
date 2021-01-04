using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    [Header("Delay between shots")]
    public float startTimeBetweenShots;
    public SpriteRenderer sanitizer;

    public GameObject[] normalAttackPrefab;
    public GameObject[] ultraAttackPrefab;

    public List<Sprite> sprites = new List<Sprite>();

    // 0 : BLUE SANITIZER
    // 1 : GREEN SANITIZER
    // 2 : RED SANITIZER

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
}
