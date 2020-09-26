using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class GameAssets : MonoBehaviour
{
    private static GameAssets _i;

    public static GameAssets i
    {
        get
        {
            if (_i == null) _i = Instantiate(Resources.Load<GameAssets>("GameAssets"));
            return _i;
        }
    }

    public Transform pfDamagePopup;

    public Dictionary<string, SpellIconImageObject> imageobjects;

    public SpellIconImageObjectMap images;

    public GameAssets()
    {
        imageobjects = new Dictionary<string, SpellIconImageObject>();

        //imageobjects.Add(,Resources.Load<Sprite>("Assets/GUI_Parts/Icons/skill_icon_03.png"));
    }
}
