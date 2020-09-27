using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : ISpell, IColliderObserver
{
    private readonly int manacost = 10;
    private readonly float coolDown = 5f;
    private float currentCoolDown = 0f;
    private readonly float duration = 0.15f;
    private readonly float radius = 5f;
    private readonly int damage = 20;
    private readonly int force = 500;
    private readonly float knockbackDuration = 0.5f;

    private Player player;
    private float remainingDuration;
    private bool invoked;
    private int oldRotationSpeed;
    private int oldMovementSpeed;
    private string name = "Dash";
    private ParticleSystem particle;
    private string animation;
    private Transform playerTransform;
    private SphereCollider chargeCollider;
    private List<Enemy> enemiesStruck;

    public Dash()
    {
        this.player = GameAssets.i.player.GetComponent<Player>();
        this.playerTransform = GameAssets.i.player.transform;
        this.particle = player.GetComponent<ParticleSystem>();
        particle.Stop();
        animation = "arthur_active_01";
        enemiesStruck = new List<Enemy>();
    }



    public float GetCoolDown()
    {
        return coolDown;
    }

    public float GetCurrentCoolDown()
    {
        return currentCoolDown;
    }

    public int GetManacost()
    {
        return manacost;
    }

    public void Invoke()
    {
        invoked = true;

        AddColider();
        player.gameObject.GetComponent<CapsuleCollider>().enabled = false;
        particle.Play();
        oldRotationSpeed = player.GetRotationSpeed();
        player.SetRotationSpeed(0);
        oldMovementSpeed = player.GetMoveSpeed();
        player.SetMoveSpeed(player.GetMoveSpeed() * 10);

        remainingDuration = duration;

        currentCoolDown = coolDown;
    }

    public void UpdateStatus()
    {
        if (invoked)
        {
            remainingDuration -= Time.deltaTime;

            if (remainingDuration <= 0)
            {
                unInvoke();
            }
        }

        if(currentCoolDown > 0)
        {
            currentCoolDown -= Time.deltaTime;
        }

        if(currentCoolDown < 0)
        {
            currentCoolDown = 0;
        }

    }

    private void unInvoke()
    {
        enemiesStruck = new List<Enemy>();
        remainingDuration = 0;
        player.SetRotationSpeed(oldRotationSpeed);
        player.SetMoveSpeed(oldMovementSpeed);
        GameAssets.i.destroyGameObject(chargeCollider);
        player.gameObject.GetComponent<CapsuleCollider>().enabled = true;
        invoked = false;
    }

    public bool isInvoked()
    {
        return invoked;
    }

    public string GetName()
    {
        return this.name;
    }

    public string GetAnimation()
    {
        return this.animation;
    }

    public void OnTriggerEnterRespond(Collider target)
    {
        if(target.tag == "Enemy")
        {
            Enemy enemy = target.gameObject.GetComponent<Enemy>();

            if (!enemiesStruck.Contains(enemy))
            {
                Vector3 direction = (target.transform.position - player.transform.position).normalized;
                enemy.ApplyKnockback(new Knockback(force, direction, knockbackDuration));
                enemy.ApplyDamage(damage);
                enemiesStruck.Add(enemy);
            }


        }
    }

    public void OnTriggerExitRespond(Collider target)
    {
        //throw new System.NotImplementedException();
    }


    private void AddColider()
    {
        GameObject spellObject = player.transform.Find("SpellObject").gameObject;

        chargeCollider = spellObject.AddComponent<SphereCollider>();
        chargeCollider.isTrigger = true;
        chargeCollider.radius = radius;



    }
}
