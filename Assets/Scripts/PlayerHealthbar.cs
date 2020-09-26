using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthbar : Healthbar
{
    [SerializeField]
    private TextMeshProUGUI text;
    public new void SetHealth(int value)
    {
        base.SetHealth(value);

        text.text = value.ToString() + "/" + maxHealth;
    }
}
