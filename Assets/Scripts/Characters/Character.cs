using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Character : MonoBehaviour
{

    public Knockback appliedKnockback;
    public int maxHealth;
    protected int currentHealth;
    // Start is called before the first frame update
    public bool isDead = false;

    [SerializeField]
    protected int healthRegeneration;

    [SerializeField]
    protected int moveSpeed;

    protected void Start()
    {

    }

    protected void Awake()
    {
        currentHealth = maxHealth;
    }

    public void ApplyDamage(int damage)
    {

        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    protected void Die()
    {
        isDead = true;
    }

    public int GetMoveSpeed()
    {
        return moveSpeed;
    }
    public void SetMoveSpeed(int newSpeed)
    {
        moveSpeed = newSpeed;
    }

    public void Update()
    {
        if (appliedKnockback != null)
        {
            appliedKnockback.remainingDuration -= Time.deltaTime;
            if (appliedKnockback.remainingDuration < 0)
            {
                appliedKnockback = null;
            }
        }
    }

    public void ApplyKnockback(Knockback knockback)
    {
        appliedKnockback = knockback;
    }
}
