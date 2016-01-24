using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    private NavMeshAgent m_NavMeshAgent;
    private Animator m_Animator;

    public Transform m_Target;

    public float m_TimeBetweenAttacks = 2f;
    public float m_NextAttackTime;
    public float myCollisionRadius = 0.35f;
	
	void Start ()
    {
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
        m_Animator = GetComponent<Animator>();
	}
	
	void Update ()
    {
        if (m_Target != null)
        {
            m_NavMeshAgent.SetDestination(m_Target.position);

            if (m_NavMeshAgent.remainingDistance <= m_NavMeshAgent.stoppingDistance)
            {
                if (Time.time > m_NextAttackTime)
                {
                    m_NextAttackTime = Time.time + m_TimeBetweenAttacks;
                    StartCoroutine(Attack());
                }
            }
        }
	}

    IEnumerator Attack()
    {
        Vector3 originalPosition = transform.position;
        Vector3 dirToTarget = (m_Target.position - transform.position).normalized;
        Vector3 attackPosition = m_Target.position - dirToTarget * (myCollisionRadius);

        float attackSpeed = 1.20f;
        float percent = 0;
        m_Animator.SetTrigger("Attack");
        while (percent <= 1)
        {
            percent += Time.deltaTime * attackSpeed;
            float interpolation = (-Mathf.Pow(percent,2) + percent) * 4;
            transform.position = Vector3.Lerp(originalPosition, attackPosition, interpolation);
            yield return null;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            m_Target = other.transform;
        }
    }
}
