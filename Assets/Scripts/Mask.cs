using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mask : MonoBehaviour
{
    public int bonusHP;

    public SpriteRenderer mask;

    public List<Sprite> sprites = new List<Sprite>();

    // INDEXES
    // 0 : SURGICAL MASK
    // 1 : KN95 MASK

    public void SelectOption(int index)
    {
        mask.sprite = sprites[index]; // change mask sprite
        PlayerPrefs.SetInt("maskEquipped", index);
        UpdateBonus();
    }

    private void UpdateBonus()
    {
        if (PlayerPrefs.GetInt("maskEquipped") == 0)
        {
            bonusHP = 50;
        }

        else if (PlayerPrefs.GetInt("maskEquipped") == 1)
        {
            bonusHP = 100;
        }
    }
    
}
