using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : ISpell
{

    private readonly int manacost = 15;
    private readonly float coolDown = 3f;
    private float currentCoolDown = 0f;
    private readonly float duration = 0.45f;
    private readonly int damage = 30;
    private readonly float windUpTime = 0.15f;
    private float remainingWindUp = 0;
    private readonly float radius = 6;

    private bool windUpDone = false;
    private Player player;
    private float remainingDuration;
    private bool invoked;
    private string name = "Swipe";
    private string animation = "arthur_swipe";
    private Transform playerSwordFx;
    private List<Enemy> enemiesStruck;


    public Swipe()
    {
        this.player = GameAssets.instance.player.GetComponent<Player>();
        Transform[] ts = GameAssets.instance.player.GetComponentsInChildren<Transform>(true);
        foreach(Transform t in ts)
        {
            if (t.name == "fx_weapon")
            {
                this.playerSwordFx = t;
            }
        }

        remainingDuration = duration;
    }


    public void UpdateStatus()
    {
        if (invoked)
        {
            remainingDuration -= Time.deltaTime;

            if(remainingWindUp <= 0 && !windUpDone)
            {
                ApplyDamageInArea();
                StartFX();
                windUpDone = true;
            }
            else
            {
                remainingWindUp -= Time.deltaTime;
            }

            if(remainingDuration <= 0)
            {
                UnInvoke();
            }
        }

        if (currentCoolDown > 0)
        {
            currentCoolDown -= Time.deltaTime;
        }

        if (currentCoolDown < 0)
        {
            currentCoolDown = 0;
        }

    }

    private void ApplyDamageInArea()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(player.transform.position, radius, GameAssets.instance.enemyLayers);
        foreach (Collider enemyCollider in hitEnemies)
        {
            Enemy enemy = enemyCollider.GetComponent<Enemy>();

            if (enemy != null)
            {
                if (IsInFrontOf(enemy.transform))
                {
                    enemy.ApplyDamage(damage);
                }
            }
        }
    }
    
    private bool IsInFrontOf(Transform target)
    {
        Vector3 targetDir = target.position - player.transform.position;

        float angle = Vector3.Angle(targetDir, player.transform.forward);

        if (Mathf.Abs(angle) < 90)
        {
            return true;
        }

        return false;
    }

    private void StartFX()
    {
        if (!this.playerSwordFx.gameObject.activeInHierarchy)
        {
            this.playerSwordFx.gameObject.SetActive(true);
        }

    }


    public string GetAnimation()
    {
        return this.animation;
    }

    public float GetCoolDown()
    {
        return this.coolDown;
    }

    public float GetCurrentCoolDown()
    {
        return this.currentCoolDown;
    }

    public int GetManacost()
    {
        return this.manacost;
    }

    public string GetName()
    {
        return this.name;
    }

    public void Invoke()
    {
        invoked = true;

        windUpDone = false;

        remainingWindUp = windUpTime;

        remainingDuration = duration;

        currentCoolDown = coolDown;
    }

    private void UnInvoke()
    {
        this.playerSwordFx.gameObject.SetActive(false);
        invoked = false;
        remainingDuration = 0;
    }

    public bool isInvoked()
    {
        return invoked;
    }
}
