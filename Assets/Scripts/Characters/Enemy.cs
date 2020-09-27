using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField]
    private Transform pfDamagePopup;
    [SerializeField]
    private Canvas canvas;

    [SerializeField]
    private int damage;

    [SerializeField]
    private float attackSpeed;

    public new void ApplyDamage(int damage)
    {
        base.ApplyDamage(damage);

        DamagePopup.Create(canvas, damage);
    }

    public int GetDamage()
    {
        return damage;
    }

    public float GetAttackSpeed()
    {
        return attackSpeed;
    }

}
