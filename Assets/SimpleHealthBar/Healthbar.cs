using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Only allow this script to be attached to the object with the healthbar slider:
[RequireComponent(typeof(Slider))]
public class Healthbar : MonoBehaviour {

    // Visible health bar ui:
    public Slider healthbarDisplay;
    public Gradient gradient;
    public Image fill;

    protected int maxHealth = 100;
    protected int currentHealth = 100;

    public void SetHealth(int value)
    {
        currentHealth = value;
        healthbarDisplay.value = currentHealth;
        fill.color = gradient.Evaluate(healthbarDisplay.normalizedValue);
    }

    public void SetMaxHealth(int value)
    {
        maxHealth = value;
        healthbarDisplay.maxValue = maxHealth;
        healthbarDisplay.value = currentHealth;
        fill.color = gradient.Evaluate(1f);
        
    }
}