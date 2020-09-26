using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : ISpell
{
    private readonly int manacost = 10;
    private readonly float coolDown = 5f;
    private float currentCoolDown = 0f;
    private Player player;
    private readonly float duration = 0.15f;
    private float remainingDuration;
    private bool invoked;
    private int oldRotationSpeed;
    private int oldMovementSpeed;
    private string name = "Dash";

    public Dash(Player player)
    {
        this.player = player;
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
                remainingDuration = 0;
                player.SetRotationSpeed(oldRotationSpeed);
                player.SetMoveSpeed(oldMovementSpeed);

                invoked = false;
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

    public bool isInvoked()
    {
        return invoked;
    }

    public string GetName()
    {
        return name;
    }
}
