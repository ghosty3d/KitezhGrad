using UnityEngine;
using System.Collections;

public class Zombie : MonoBehaviour {

    public Animator m_Animator;
    public SphereCollider m_Trigger;

    public Transform m_Traget;

    public enum EnemyStates
    {
        Idle,
        StandUp,
        Walk,
        Hit,
        Attack
    }

    public EnemyStates m_CurrentEnemyState;

    // Use this for initialization
	void Start ()
    {
        m_Trigger = GetComponent<SphereCollider>();
        m_Trigger.isTrigger = true;

        m_Animator = GetComponent<Animator>();

        m_CurrentEnemyState = EnemyStates.Idle;
	}
	
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            m_Animator.SetTrigger("StandUp");
            m_CurrentEnemyState = EnemyStates.StandUp;

            m_Traget = other.transform;
        }
    }

    void Update()
    {
        if (m_CurrentEnemyState != EnemyStates.Idle && m_Traget != null)
        {
            transform.LookAt(m_Traget.position);
        }
    }
}
