using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Player player;
    public Rigidbody rigidbody;
    // Start is called before the first frame 
    public Animator animator;
    public bool isAttacking;

    private float timeUntilRegen = 1f;
    // Update is called once per frame
    
    [SerializeField]
    private SpellIconController[] spellIcons;

    private void Start()
    {
        for(int i = 0; i < spellIcons.Length; i++)
        {
            if (i < player.GetSpells().Count)
            {
                spellIcons[i].SetSpell(player.GetSpells()[i]);
            }

        }
    }
    void Update()
    {
        handleMovement();

        float attack = Input.GetAxis("Fire1");
        
        if(attack > 0)
        {

            animator.SetBool("isAttacking", true);
        }
        else
        {
            animator.SetBool("isAttacking", false);
        }

        UpdateSpells();


        timeUntilRegen -= Time.deltaTime;
        if(timeUntilRegen < 0)
        {
            player.TriggerRegen();
            timeUntilRegen = 1;
        }
    }

    void handleMovement()
    {
        float vx = Input.GetAxis("Horizontal");
        float vz = Input.GetAxis("Vertical");

        if (vx != 0 || vz != 0)
        {

            var velocity = new Vector3(vx, 0, vz);

            float newVx = vx * player.GetMoveSpeed() / velocity.magnitude;
            float newVz = vz * player.GetMoveSpeed() / velocity.magnitude;

            if (Mathf.Abs(Vector3.Dot(transform.forward.normalized, velocity.normalized) - 1) > 0.001)
            {

                rigidbody.velocity = new Vector3(0, 0, 0);

                transform.forward = Vector3.RotateTowards(transform.forward, velocity.normalized, 0.03f * player.GetRotationSpeed(), 0);
            }
            else
            {
                rigidbody.velocity = new Vector3(newVx, 0, newVz);
            }

        }
        else
        {
            rigidbody.velocity = new Vector3(0, 0, 0);
        }
        
 
        animator.SetFloat("speed", rigidbody.velocity.magnitude);

    }

    public void SetIsAttacking(int isAttacking)
    {
        if (isAttacking == 0)
        {
            this.isAttacking = false;
            GameObject.Find("Bip001 Prop1").GetComponent<SwordController>().targetStruck = false;
        }
        else
        {
            this.isAttacking = true;
        }
    }

    public void InvokeSpell(int index)
    {
        ISpell spell = player.GetSpells()[index];
        if (player.AttemptSpellInvoke(index))
        {
            animator.Play(spell.GetAnimation());
        }
        
        
    }

    private void UpdateSpells()
    {
        foreach (ISpell spell in player.GetSpells())
        {
            spell.UpdateStatus();
        }
    }

}


