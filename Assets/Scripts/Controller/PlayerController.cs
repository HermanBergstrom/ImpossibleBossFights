using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

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
    private Enemy hoveredEnemy; 
    private Enemy target;
    [SerializeField]
    private SpellIconController[] spellIcons;
    private bool controllsDisabled;
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
        movement.SetMaxSpeed(player.GetMoveSpeed());
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

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 100))
        {
            CheckForHoveredEnemy(hit);
        }

        if (Input.GetMouseButton(0))
        {
            if(hoveredEnemy != null)
            {
                target = hoveredEnemy;
            }
            else
            {
                target = null;
                MovePlayerToPoint(hit.point);
            }
        }

        if (target != null)
        {
            HandleAttackCommand();
        }

    }

    public void MovePlayerToPoint(Vector3 point)
    {
        if (!controllsDisabled)
        {
            movement.SetStopped(false);
            movement.MoveToPoint(point);
        }
    }
    private void HandleAttackCommand()
    {
        if (!controllsDisabled)
        {
            if (Vector3.Distance(player.transform.position, target.transform.position) < player.GetAttackRange())
            {

                FaceTarget(target.transform);
                if (IsFacingTarget(target.transform))
                {
                    animator.SetTrigger("attacking");
                    target = null;
                    movement.SetStopped(true);
                }

            }
            else
            {
                MovePlayerToPoint(target.transform.position);
            }
        }
    }

    private void CheckForHoveredEnemy(RaycastHit hit)
    {

        Enemy enemy = hit.collider.GetComponent<Enemy>();

        if (enemy != null)
        {
            hoveredEnemy = enemy;

            AddTargetOutline();
        }
        else
        {
            if (hoveredEnemy != null)
            {
                hoveredEnemy.gameObject.GetComponent<Outline>().enabled = false;
                hoveredEnemy = null;
            }
        }
        
    }

    private void AddTargetOutline()
    {
        if (hoveredEnemy.GetComponent<Outline>() == null)
        {
            Outline outline = hoveredEnemy.gameObject.AddComponent<Outline>();
            Color red = Color.red;
            red.a = 0.4f;
            outline.OutlineColor = red;
        }
        hoveredEnemy.gameObject.GetComponent<Outline>().enabled = true;
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

    bool IsFacingTarget(Transform target)
    {
        Vector3 targetDir = target.position - transform.position;

        float angle = Vector3.Angle(targetDir, transform.forward);

        if(Mathf.Abs(angle) < 20)
        {
            return true;
        }

        return false;
    }

    public void SetControllsDisabled(bool value)
    {
        controllsDisabled = value;
    }
}


