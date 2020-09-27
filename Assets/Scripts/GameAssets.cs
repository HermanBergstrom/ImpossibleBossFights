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

    public GameObject player;
    private void Awake()
    {
        imageobjects = new Dictionary<string, SpellIconImageObject>();

        Sprite defaultImage = Resources.Load<Sprite>("Icons/skill_icon_03");
        Sprite pressedImage = Resources.Load<Sprite>("Icons/skill_icon_03_nobg");
        imageobjects.Add("Dash", new SpellIconImageObject(defaultImage, pressedImage));

        player = GameObject.Find("Player");
        
    }

    public void destroyGameObject(UnityEngine.Object item)
    {
        Destroy(item);
    }

}
