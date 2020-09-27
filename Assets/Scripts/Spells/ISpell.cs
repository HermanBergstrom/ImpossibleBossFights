using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpell
{
    int GetManacost();

    float GetCoolDown();

    float GetCurrentCoolDown();

    void Invoke();
    void UpdateStatus();
    bool isInvoked();
    string GetName();
    string GetAnimation();

}
