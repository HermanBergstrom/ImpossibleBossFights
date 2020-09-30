using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;

[RequireComponent(typeof(CursorMovementScript))]
public class PlayerController : MonoBehaviour
{

    public Player player;
    public Rigidbody rigidbody;
    private CursorMovementScript movement;
    // Start is called before the first frame 
    public Animator animator;
    private bool isAttacking;
    private float timeUntilRegen = 1f;
    // Update is called once per frame
    private Camera cam;
    private Enemy target;
    [SerializeField]
    private SpellIconController[] spellIcons;

    public AudioSource playerAudio;

    private void Start()
    {
        cam = Camera.main;

        movement = GetComponent<CursorMovementScript>();

        AddSpells();

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


        animator.SetFloat("speed", movement.GetCurrentSpeed());
        HandleCurrentMouseAction();
        UpdateSpells();
        UpdateRegen();

    }



    void UpdateRegen() {
        timeUntilRegen -= Time.deltaTime;
        if (timeUntilRegen < 0)
        {
            player.TriggerRegen();
            timeUntilRegen = 1;
        }
    }

    void HandleCurrentMouseAction()
    {
        bool attacking = false;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            Enemy enemy = hit.collider.GetComponent<Enemy>();

            if (enemy != null)
            {
                target = enemy;

                AddTargetOutline();

                if (Input.GetMouseButton(0))
                {
                    if (Vector3.Distance(player.transform.position, enemy.transform.position) < player.GetAttackRange())
                    {
                        
                        FaceTarget(enemy.transform);
                        animator.SetTrigger("attacking");
                        movement.SetStopped(true);
                        attacking = true;
                    }

                }
            }
            else
            {
                if (target != null)
                {
                    target.gameObject.GetComponent<Outline>().enabled = false;
                    target = null;
                }
            }
            if (Input.GetMouseButton(0) && !attacking)
            {
                movement.SetStopped(false);
                movement.MoveToPoint(hit.point);
            }
        }
    }

    private void AddTargetOutline()
    {
        if (target.GetComponent<Outline>() == null)
        {
            Outline outline = target.gameObject.AddComponent<Outline>();
            Color red = Color.red;
            red.a = 0.4f;
            outline.OutlineColor = red;
        }
        target.gameObject.GetComponent<Outline>().enabled = true;
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
            playerAudio.clip = Resources.Load<AudioClip>("Sounds/Spells/" + spell.GetName());
            playerAudio.Play();
        }
        
        
    }

    private void UpdateSpells()
    {
        foreach (ISpell spell in player.GetSpells())
        {
            spell.UpdateStatus();
        }
    }




    private void AddSpells()
    {
        Dash dash = new Dash();
        Swipe swipe = new Swipe();

        player.AddSpell(swipe);
        player.AddSpell(dash);
    }

    public bool GetIsAttacking()
    {
        return isAttacking;
    }

    void FaceTarget(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
    }
}


