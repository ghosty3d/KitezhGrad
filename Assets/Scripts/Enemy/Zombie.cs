using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Zombie : MonoBehaviour {

    public ZombieHealth m_ZombieHealth;

    public Animator m_Animator;
    public AudioSource m_AudioSource;

    public List<AudioClip> Growls = new List<AudioClip>();
    public List<AudioClip> Hurts = new List<AudioClip>();
    public List<AudioClip> Moans = new List<AudioClip>();

    public CapsuleCollider m_Collider;
    public SphereCollider m_Trigger;

    public Transform m_Traget;

    public int m_CurrentHealth;
    public int m_MaxHealth = 100;

    public bool isDead = false;

    public enum EnemyStates
    {
        Idle,
        StandUp,
        Walk,
        Hit,
        Attack,
        Dead
    }

    public EnemyStates m_CurrentEnemyState;

    // Use this for initialization
	void Start ()
    {
        m_Trigger = GetComponent<SphereCollider>();
        m_Trigger.isTrigger = true;

        m_Collider = GetComponent<CapsuleCollider>();
        m_Collider.isTrigger = false;
        m_Collider.enabled = false;

        m_Animator = GetComponent<Animator>();
        m_Animator.SetBool("Dead", isDead);

        m_AudioSource = GetComponent<AudioSource>();

        m_ZombieHealth = GetComponent<ZombieHealth>();

        m_CurrentEnemyState = EnemyStates.Idle;

        m_CurrentHealth = m_MaxHealth;
	}
	
    void OnTriggerEnter(Collider other)
    {
        if (!isDead)
        {
            if(other.tag == "Player")
            {
                m_Animator.SetTrigger("StandUp");
                m_CurrentEnemyState = EnemyStates.StandUp;

                m_Traget = other.transform;
                m_Collider.enabled = true;
                m_Trigger.enabled = false;

                PlaySound(Growls);
            }  
        }
    }

    void Update()
    {
        if (!isDead)
        {
            if (m_CurrentEnemyState != EnemyStates.Idle && m_Traget != null)
            {
                transform.LookAt(m_Traget.position);
            }  
        }
    }

    public void AjustHealth(int a_value)
    {
        m_CurrentHealth += a_value;

        if (m_CurrentHealth <= 0)
        {
            isDead = true;
            m_Animator.SetTrigger("Die");
            m_Animator.SetBool("Dead", isDead);
            m_Collider.enabled = false;
            PlaySound(Moans);
        }
        else
        {
            m_Animator.SetTrigger("Hit");
            PlaySound(Hurts);
        }
    }

    public void PlaySound(List<AudioClip> a_ClipsList)
    {
        System.Random prng = new System.Random();
        int l_Index = prng.Next(0, a_ClipsList.Count);
        m_AudioSource.clip = a_ClipsList[l_Index];
        m_AudioSource.Play();
    }
}
