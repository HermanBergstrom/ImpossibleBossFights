using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent(typeof(Enemy))]
public class EnemyController : MonoBehaviour
{
    public float lookRadius = 10f;

    Transform target;
    NavMeshAgent agent;
    Rigidbody rigidbody;
    private float attackCoolDown = 0;
    Player player;
    Enemy enemy;
    [SerializeField]
    private Animator animator;
    private bool applyingKnockback = false;
    private readonly float attackDamageDelay = 0.5f;
    private float attackDamageCountDown = 0;
    private bool isAttacking = false;
    private float bodyDespawnTimer = 10f;
    private bool deathSequenceInitiated = false;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        enemy = GetComponent<Enemy>();
        rigidbody = GetComponent<Rigidbody>();
        target = GameAssets.i.player.transform;
        player = GameAssets.i.player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!enemy.isDead)
        {
            if (attackDamageCountDown > 0 && isAttacking)
            {
                attackDamageCountDown -= Time.deltaTime;
            }
            else if (attackDamageCountDown <= 0 && isAttacking)
            {
                AttackTarget();
                StopAttack();
            }



            if (enemy.appliedKnockback != null && !applyingKnockback)
            {
                agent.enabled = false;
                rigidbody.isKinematic = false;
                rigidbody.AddForce(enemy.appliedKnockback.direction * enemy.appliedKnockback.force, ForceMode.Impulse);
                applyingKnockback = true;
            }
            else if (enemy.appliedKnockback == null && applyingKnockback)
            {
                rigidbody.isKinematic = true;
                agent.enabled = true;
                applyingKnockback = false;
            }

            if (!applyingKnockback)
            {
                float distance = Vector3.Distance(target.position, transform.position);

                if (distance <= lookRadius)
                {
                    agent.SetDestination(new Vector3(target.position.x, transform.position.y, target.position.z));

                    if (distance <= agent.stoppingDistance)
                    {
                        FaceTarget();
                        if (attackCoolDown <= 0 && !isAttacking)
                        {
                            BeginAttack();
                        }
                        animator.SetBool("isWalking", false);
                    }
                    else
                    {
                        animator.SetBool("isWalking", true);
                    }
                }
                else
                {
                    animator.SetBool("isWalking", false);
                }

                if (attackCoolDown > 0)
                {
                    attackCoolDown -= Time.deltaTime;
                }
                if (attackCoolDown < 0)
                {
                    attackCoolDown = 0;
                }
            }
        }
        else if (!deathSequenceInitiated)
        {
            animator.Play("Death");
            deathSequenceInitiated = true;
            
        }
        else
        {
            bodyDespawnTimer -= Time.deltaTime;
            if (bodyDespawnTimer <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void StopAttack()
    {
        isAttacking = false;
        attackDamageCountDown = 0;
        animator.SetBool("isAttacking", false);
    }

    private void BeginAttack()
    {
        attackDamageCountDown = attackDamageDelay;
        isAttacking = true;
        int animation = Random.Range(1, 4);
        animator.SetInteger("randomAttack", animation);
        animator.speed = enemy.GetAttackSpeed();
        animator.SetBool("isAttacking", true);
    }

    void AttackTarget()
    {
        player.ApplyDamage(enemy.GetDamage());
        attackCoolDown = 1/enemy.GetAttackSpeed();
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime*5);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
