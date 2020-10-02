using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dash : ISpell
{
    private readonly int manacost = 10;
    private readonly float coolDown = 5f;
    private float currentCoolDown = 0f;
    //private readonly float duration = 0.2f;
    private readonly float radius = 5f;
    private readonly int damage = 20;
    private readonly int force = 500;
    private readonly float knockbackDuration = 0.5f;
    private readonly int distance = 20;

    private Player player;
    //private float remainingDuration;
    private bool invoked;
    private int oldMovementSpeed;
    private string name = "Dash";
    private ParticleSystem particle;
    private string animation;
    private List<Enemy> enemiesStruck;
    private PlayerController playerController;
    private Vector3 targetPosition;

    public Dash()
    {
        this.player = GameAssets.instance.player.GetComponent<Player>();
        this.playerController = player.GetComponent<PlayerController>();
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

        player.gameObject.GetComponent<CapsuleCollider>().enabled = false;
        particle.Play();
        oldMovementSpeed = player.GetMoveSpeed();
        player.SetMoveSpeed(player.GetMoveSpeed() * 10);

        targetPosition = FindChargePoint();
        playerController.MovePlayerToPoint(targetPosition);
        playerController.SetMovementControllerDisabled(true);

        //remainingDuration = duration;

        currentCoolDown = coolDown;
    }

    private Vector3 FindChargePoint()
    {
        NavMeshHit hit;
        Vector3 output = player.transform.position;

        for (int i = 0; i <= distance; i++)
        {
            NavMesh.SamplePosition(player.transform.position + player.transform.forward * i, out hit, 1, -1);
            if (hit.hit)
            {
                output = hit.position;
            }
            else
            {
                break;
            }
        }

        return output;
    }

    public void UpdateStatus()
    {
        if (invoked)
        {
            //remainingDuration -= Time.deltaTime;
            checkForEnemyColission();

            if (Vector3.Distance(player.transform.position, targetPosition) < 0.1)
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

    private void checkForEnemyColission()
    {

        Collider[] hitEnemies = Physics.OverlapSphere(player.transform.position, radius, GameAssets.instance.enemyLayers);
        foreach(Collider enemyCollider in hitEnemies)
        {
            Enemy enemy = enemyCollider.GetComponent<Enemy>();

            if (enemy != null && !enemiesStruck.Contains(enemy))
            {
                Vector3 direction = (enemy.transform.position - player.transform.position).normalized;
                enemy.ApplyKnockback(new Knockback(force, direction, knockbackDuration));
                enemy.ApplyDamage(damage);
                enemiesStruck.Add(enemy);
            }
        }
    }

    private void unInvoke()
    {
        enemiesStruck = new List<Enemy>();
        //remainingDuration = 0;
        player.SetMoveSpeed(oldMovementSpeed);
        player.gameObject.GetComponent<CapsuleCollider>().enabled = true;
        playerController.SetMovementControllerDisabled(false);
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
}
