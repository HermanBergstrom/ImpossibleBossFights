using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int maxHealth;
    protected int currentHealth;
    // Start is called before the first frame update

    [SerializeField]
    protected int healthRegeneration;

    [SerializeField]
    protected int moveSpeed;



    protected void Start()
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

    private void Die()
    {
        Destroy(gameObject);
    }

    public int GetMoveSpeed()
    {
        return moveSpeed;
    }
    public void SetMoveSpeed(int newSpeed)
    {
        moveSpeed = newSpeed;
    }
}
