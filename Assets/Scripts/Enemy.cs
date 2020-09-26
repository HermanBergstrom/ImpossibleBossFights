using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField]
    private Healthbar hpbar;
    [SerializeField]
    private Transform pfDamagePopup;
    [SerializeField]
    private Canvas canvas;
    private void Start()
    {
        base.Start();
        hpbar.SetMaxHealth(this.maxHealth);
        hpbar.SetHealth(this.currentHealth);
    }

    public new void ApplyDamage(int damage)
    {
        base.ApplyDamage(damage);

        hpbar.SetHealth(this.currentHealth);

        DamagePopup.Create(canvas, damage);
    }

    private void Update()
    {
        
    }
}
