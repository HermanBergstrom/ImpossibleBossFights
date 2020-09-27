using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellIconImageObject 
{


    public Sprite defaultImage;
    public Sprite pressedImage;

    public SpellIconImageObject(Sprite defaultImage, Sprite pressedImage)
    {
        this.defaultImage = defaultImage;
        this.pressedImage = pressedImage;
    }
}
