using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    public Slider Slider;


    
    public void Initialize(float startingHealth )
    {
        Slider.maxValue = startingHealth;
        Slider.value = startingHealth;
    }

    public void SetValue(float currentHealth)
    {

        Slider.value = currentHealth;
    }

}
