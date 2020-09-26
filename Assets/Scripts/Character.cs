using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int maxHealth;
    protected int currentHealth;
    // Start is called before the first frame update
    protected void Start()
    {
        currentHealth = maxHealth;
        Debug.Log(currentHealth);
    }

    public void ApplyDamage(int damage)
    {

        Debug.Log(currentHealth);
        currentHealth -= damage;

        Debug.Log(currentHealth);

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
