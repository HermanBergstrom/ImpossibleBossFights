using System;
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
    [SerializeField]
    private int manaRegeneration;

    [SerializeField]
    protected int rotationSpeed;

    private List<ISpell> spells;

    private bool isMooving;

    public new void Awake()
    {
        base.Awake();

        currentMana = maxMana;

        SetupBars();

        spells = new List<ISpell>();
    }

    public new void ApplyDamage(int damage)
    {
        base.ApplyDamage(damage);

        hpbar.SetHealth(this.currentHealth);

    }

    private void SetupBars()
    {
        hpbar.SetMaxHealth(this.maxHealth);
        hpbar.SetHealth(this.currentHealth);


        manabar.SetMaxMana(this.maxMana);
        manabar.SetMana(this.currentMana);
    }

    public int GetRotationSpeed()
    {
        return rotationSpeed;
    }

    public void SetRotationSpeed(int newSpeed)
    {
        rotationSpeed = newSpeed;
    }

    public bool AttemptSpellInvoke(int index)
    {

        ISpell spell = spells[index];

        if (spell == null)
        {
            return false;
        }

        if (spell.GetManacost() > currentMana || spell.GetCurrentCoolDown() > 0)
        {
            return false;
        }

        UseMana(spell.GetManacost());
        spell.Invoke();

        return true;
    }

    private void UseMana(int amount)
    {
        currentMana -= amount;
        manabar.SetMana(currentMana);
    }

    public void TriggerRegen()
    {
        RegenHealth();
        RegenMana();
    }

    private void RegenHealth()
    {
        if (currentHealth < maxHealth)
        {
            if (currentHealth + healthRegeneration > maxHealth)
            {
                currentHealth = maxHealth;
            }
            else
            {
                currentHealth += healthRegeneration;
            }
        }
        hpbar.SetHealth(currentHealth);
    }
    public void RegenMana()
    {
        if(currentMana < maxMana)
        {
            if(currentMana + manaRegeneration > maxMana)
            {
                currentMana = maxMana;
            }
            else
            {
                currentMana += manaRegeneration;
            }
        }

        manabar.SetMana(currentMana);
    }

    public List<ISpell> GetSpells()
    {
        return spells;
    }

    public bool IsMooving()
    {
        return isMooving;
    }

    public void SetIsMooving(bool newValue)
    {
        isMooving = newValue;
    }

    public int GetCurrentMana()
    {
        return currentMana;
    }

    public void AddSpell(ISpell spell, int index)
    {
        spells[index] = spell;
    }

    public void AddSpell(ISpell spell)
    {
        spells.Add(spell);
    }
}
