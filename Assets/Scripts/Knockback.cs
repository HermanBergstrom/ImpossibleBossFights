using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback
{
    public readonly int force;
    public readonly Vector3 direction;
    public float remainingDuration;

    public Knockback(int force, Vector3 direction, float remainingDuration)
    {
        this.force = force;
        this.direction = direction;
        this.remainingDuration = remainingDuration;
    }
}
