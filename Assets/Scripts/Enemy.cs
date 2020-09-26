using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{

    public Healthbar hpbar;
    private void Start()
    {
        base.Start();
    }

    public new void ApplyDamage(int damage)
    {
        base.ApplyDamage(damage);

        hpbar.SetHealth(this.currentHealth);

    }

    private void Update()
    {
        
    }
}
