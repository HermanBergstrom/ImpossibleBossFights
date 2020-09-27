using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputController : MonoBehaviour
{

    public Button[] spells;
    public Sprite[] defaultImages;
    public Sprite[] pressedImages;
    public PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            spells[0].image.sprite = pressedImages[0];
        }
        if (Input.GetKey(KeyCode.Alpha1))
        {
            playerController.InvokeSpell(0);
        }
        if (Input.GetKeyUp(KeyCode.Alpha1)){
            spells[0].image.sprite = defaultImages[0];
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            spells[1].image.sprite = pressedImages[1];
        }
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            spells[1].image.sprite = defaultImages[1];
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            spells[2].image.sprite = pressedImages[2];
        }
        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            spells[2].image.sprite = defaultImages[2];
        }
        if (Input.GetKey(KeyCode.Alpha4))
        {
            spells[3].image.sprite = pressedImages[3];
        }
        if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            spells[3].image.sprite = defaultImages[3];
        }
    }
}
