using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerManaBar : MonoBehaviour
{
    // Visible health bar ui:
    public Slider manaBarDisplay;
    public Gradient gradient;
    public Image fill;
    [SerializeField]
    private TextMeshProUGUI text;

    protected int maxMana = 100;
    protected int currentMana = 100;

    public void SetMana(int value)
    {
        currentMana = value;
        manaBarDisplay.value = currentMana;
        fill.color = gradient.Evaluate(manaBarDisplay.normalizedValue);
        text.text = value.ToString() + "/" + maxMana;
    }

    public void SetMaxMana(int value)
    {
        maxMana = value;
        manaBarDisplay.maxValue = maxMana;
        manaBarDisplay.value = currentMana;
        fill.color = gradient.Evaluate(1f);

    }
}
