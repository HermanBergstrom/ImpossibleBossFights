using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    private bool applyingKnockback = false;

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
        if (enemy.appliedKnockback != null && !applyingKnockback)
        {
            agent.enabled = false;
            rigidbody.isKinematic = false;
            rigidbody.AddForce(enemy.appliedKnockback.direction * enemy.appliedKnockback.force, ForceMode.Impulse);
            applyingKnockback = true;
        }else if (enemy.appliedKnockback == null && applyingKnockback)
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
                    if (attackCoolDown <= 0)
                    {
                        AttackTarget();
                    }
                }
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

    void AttackTarget()
    {
        player.ApplyDamage(enemy.GetDamage());
        attackCoolDown = enemy.GetAttackSpeed();
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
