using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private int currentMana;

    [SerializeField]
    private int maxMana;

    [SerializeField]
    private PlayerHealthbar hpbar;

    [SerializeField]
    private PlayerManaBar manabar;
    // Start is called before the first frame update
    private new void Start()
    {
        base.Start();
        hpbar.SetMaxHealth(this.maxHealth);
        this.currentHealth = 40;
        hpbar.SetHealth(this.currentHealth);


        manabar.SetMaxMana(this.maxMana);
        this.currentMana = 30;
        manabar.SetMana(this.currentMana);
    }

    public new void ApplyDamage(int damage)
    {
        base.ApplyDamage(damage);

        hpbar.SetHealth(this.currentHealth);

    }

}
