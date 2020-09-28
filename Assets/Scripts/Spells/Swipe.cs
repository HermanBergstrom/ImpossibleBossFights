using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : ISpell
{

    private readonly int manacost = 15;
    private readonly float coolDown = 3f;
    private float currentCoolDown = 0f;
    private readonly float duration = 0.65f;
    private readonly int damage = 30;

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

            if (remainingDuration <= 0)
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
        this.playerSwordFx.gameObject.SetActive(true);
        invoked = true;

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
