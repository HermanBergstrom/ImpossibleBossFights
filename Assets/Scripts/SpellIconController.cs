using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellIconController : MonoBehaviour
{

    private ISpell spell;
    Image image;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSpell(ISpell spell)
    {
        this.spell = spell;
    }
}
