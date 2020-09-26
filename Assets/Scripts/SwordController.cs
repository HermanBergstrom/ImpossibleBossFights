using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    [NonSerialized]
    public bool targetStruck = false;

    // Update is called once per frame
    public int damage;


    void Update()
    {
        
    }

    void OnTriggerEnter(Collider target)
    {
        if (target.tag == "Enemy" && GameObject.Find("Player").GetComponent<PlayerController>().isAttacking && !targetStruck)
        {
            target.gameObject.GetComponent<Enemy>().ApplyDamage(damage);
            targetStruck = true;
        }
    }

    void OnTriggerExit(Collider target)
    {

    }

}
