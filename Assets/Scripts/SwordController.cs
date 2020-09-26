using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    [NonSerialized]
    public bool targetStruck = false;
    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider target)
    {
        if (target.tag == "Enemy" && GameObject.Find("Player").GetComponent<CharacterController>().isAttacking && !targetStruck)
        {
            Debug.Log("I HIT HIM");
            targetStruck = true;
        }
    }

    void OnTriggerExit(Collider target)
    {

    }
}
