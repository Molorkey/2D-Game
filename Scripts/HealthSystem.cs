using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    private Slider slider;

    private void Awake()
    {
        slider = GameObject.Find("HealthSystem").GetComponent<Slider>();


    }

    public void SetHealth(int maxHealth)
    {
        slider.minValue = 0;
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
    }

    public void UpdateHealth(int currentHealth)
    {
        slider.value = currentHealth;
    }



}
