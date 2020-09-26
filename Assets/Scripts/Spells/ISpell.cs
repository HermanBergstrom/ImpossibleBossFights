using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface ISpell
{
    int GetManacost();

    float GetCoolDown();

    float GetCurrentCoolDown();

    void Invoke();
    void UpdateStatus();
    bool isInvoked();

}
