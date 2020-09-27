using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpellIconController : MonoBehaviour
{

    private ISpell spell;
    private SpellIconImageObject imageObject;
    [SerializeField]
    private Button button;
    [SerializeField]
    private Image image;
    [SerializeField]
    private TextMeshProUGUI coolDownText;
    [SerializeField]
    private Slider coolDownFilter;
    [SerializeField]
    private Image manaFilter;
    [SerializeField]
    private Player player;
    [SerializeField]
    private TextMeshProUGUI manaCostText;

    // Update is called once per frame
    void Update()
    {
        if(spell != null)
        {
            checkCoolDown();
            checkMana();
        }
    }

    private void checkMana()
    {
        if (spell.GetManacost() > player.GetCurrentMana())
        {
            manaFilter.gameObject.SetActive(true);
        }
        else
        {
            manaFilter.gameObject.SetActive(false);
        }
    }
    private void checkCoolDown()
    {
        if (spell.GetCurrentCoolDown() > 0)
        {
            string coolDownFormat = "0";

            if (spell.GetCurrentCoolDown() < 1)
            {
                coolDownFormat = "0.0";
            }

            coolDownText.text = spell.GetCurrentCoolDown().ToString(coolDownFormat);

            coolDownFilter.value = spell.GetCurrentCoolDown() / spell.GetCoolDown();

        }
        else
        {
            coolDownFilter.value = 0;
            coolDownText.text = "";
        }
    }

    public void SetSpell(ISpell spell)
    {
        this.spell = spell;
        imageObject = GameAssets.i.imageobjects[spell.GetName()];
        manaCostText.text = spell.GetManacost().ToString();
    }
}
