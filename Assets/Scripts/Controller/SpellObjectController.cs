using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellObjectController : MonoBehaviour
{
    public List<IColliderObserver> observers;

    void OnTriggerEnter(Collider target)
    {
        foreach (IColliderObserver observer in observers)
        {
            observer.OnTriggerEnterRespond(target);
        }
    }

    void OnTriggerExit(Collider target)
    {
        foreach (IColliderObserver observer in observers)
        {
            observer.OnTriggerExitRespond(target);
        }
    }
}
