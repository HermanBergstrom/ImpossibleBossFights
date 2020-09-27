using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IColliderObserver
{
    void OnTriggerEnterRespond(Collider target);

    void OnTriggerExitRespond(Collider target);

}