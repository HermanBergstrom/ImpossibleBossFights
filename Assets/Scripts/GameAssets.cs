using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        imageobjects.Add()
    }
}
