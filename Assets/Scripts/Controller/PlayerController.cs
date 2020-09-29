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
    List<IColliderObserver> observers;
    private float timeUntilRegen = 1f;
    // Update is called once per frame
    public NavMeshAgent agent;
    private Camera cam;
    private Enemy target;
    [SerializeField]
    private SpellIconController[] spellIcons;

    public AudioSource playerAudio;

    private void Start()
    {
        cam = Camera.main;

        movement = GetComponent<CursorMovementScript>();

        observers = new List<IColliderObserver>();

        AddSpells();

        GameObject spellObject = transform.Find("SpellObject").gameObject;

        SpellObjectController spellObjectController = spellObject.GetComponent<SpellObjectController>();

        spellObjectController.observers = observers;

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

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            Enemy enemy = hit.collider.GetComponent<Enemy>();

            if (enemy != null)
            {
                target = enemy;

                Outline outline = enemy.gameObject.AddComponent<Outline>();
                Color red = Color.red;
                red.a = 0.4f;
                outline.OutlineColor = red;
                //outline.OutlineWidth = 1f;
            }
            else
            {
                if (target != null)
                {
                    Destroy(target.gameObject.GetComponent<Outline>());
                    target = null;
                }
            }



            if (Input.GetMouseButton(0))
            {

                animator.SetBool("isAttacking", true);
                movement.MoveToPoint(hit.point);
            }
            else
            {
                animator.SetBool("isAttacking", false);
            }

        }

        agent.speed = player.GetMoveSpeed();

        animator.SetFloat("speed", agent.velocity.magnitude);

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

    void handleMovement()
    {
        float vx = Input.GetAxis("Horizontal");
        float vz = Input.GetAxis("Vertical");

        if (vx != 0 || vz != 0)
        {

            var velocity = new Vector3(vx, 0, vz);

            float newVx = vx * player.GetMoveSpeed() / velocity.magnitude;
            float newVz = vz * player.GetMoveSpeed() / velocity.magnitude;

            if (Mathf.Abs(Vector3.Dot(transform.forward.normalized, velocity.normalized) - 1) > 0.1)
            {


                //rigidbody.velocity = new Vector3(0, 0, 0);
            }
            else
            {
                rigidbody.velocity = new Vector3(newVx, 0, newVz);
            }
            transform.forward = Vector3.RotateTowards(transform.forward, velocity.normalized, 0.03f * player.GetRotationSpeed(), 0);

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
        observers.Add(dash);
        player.AddSpell(dash);

        Swipe swipe = new Swipe();
        player.AddSpell(swipe);
    }

    public bool GetIsAttacking()
    {
        return isAttacking;
    }

}


