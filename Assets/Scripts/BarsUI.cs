using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarsUI : MonoBehaviour
{
    public Slider slider;

    public void SetMaxValue(int value)
    {
        slider.maxValue = value;
        slider.value = value; //at the beginning, the bar is full
    }

    public void SetValue(int value)
    {
        slider.value = value; //update slider's current value
    }

    public void Increment()
    {
        slider.value += 1;
        Debug.Log(slider.value);
    }
}
