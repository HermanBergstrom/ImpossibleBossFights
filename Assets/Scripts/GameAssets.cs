using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class GameAssets : MonoBehaviour
{
    private static GameAssets _instance;

    public static GameAssets instance
    {
        get
        {
            if (_instance == null) _instance = Instantiate(Resources.Load<GameAssets>("GameAssets"));
            return _instance;
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

        defaultImage = Resources.Load<Sprite>("Icons/skill_icon_02");
        pressedImage = Resources.Load<Sprite>("Icons/skill_icon_02_nobg");
        imageobjects.Add("Swipe", new SpellIconImageObject(defaultImage, pressedImage));

        player = GameObject.Find("Player");
        
    }

    public void destroyGameObject(UnityEngine.Object item)
    {
        Destroy(item);
    }

}
