using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
	[SerializeField]
	private Rigidbody m_Rigidbody;
	[SerializeField]
	private Animator m_Animator;
	[SerializeField]
	private AudioSource m_AudioSource;

    public AudioClip WaterFootSteps;
    public AudioClip ShotGunShoot;

    [Range(0, 20)]
    public float ViewRadus;
    [Range(0, 360)]
    public float ViewAngle;

    public float MoveSpeed = 10f;
    public float RotationSpeed = 50f;

    public GameObject m_MuzzleFlashObject;
    public float m_MuzzleFlashTime;

    public float m_AttackTimer = 1f;
    public float m_Timer = 0f;

    public int m_WeaponDamage = 50;

    private int m_AmmoCountNow = 0;
    public int AmmoCountNow
    {
        get
        {
            return m_AmmoCountNow;
        }
        set
        {
            m_AmmoCountNow = value;
            GameCanvas.Instance.m_WeaponsPanel.UpdateAmmoCount(value, AmmoCountMax);
        }
    }

    private int m_AmmoCountMax = 0;
    public int AmmoCountMax
    {
        get
        {
            return m_AmmoCountMax;
        }
        set
        {
            m_AmmoCountMax = value;
            GameCanvas.Instance.m_WeaponsPanel.UpdateAmmoCount(AmmoCountNow, value);
        }
    }

    public List<Transform> Tartegs = new List<Transform>();
    public Zombie m_CurrentTarget = null;

    private float horizontal;
    private float vertical;

    public Vector3 m_WeaponRaycastPosition;

    public LayerMask m_TargetMask;
    public LayerMask m_ObstacleMask;

    public enum PlayerStates
    {
        Idle,
        Walk,
        Aim,
        Shoot,
        Hit,
        Dead
    }

    public PlayerStates m_CurrentPlayerState;

	public delegate void OnFireAction();
	public static event OnFireAction OnFire;

    void Awake()
    {
        Instance = this;
    }

	void Start () 
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_AudioSource = GetComponent<AudioSource>();

        m_CurrentPlayerState = PlayerStates.Idle;

        m_MuzzleFlashObject.SetActive(false);

        m_Timer = m_AttackTimer;

        //Set Ammo
        AmmoCountNow = 2;
        AmmoCountMax = 100;
	}
	
	// Update is called once per frame
	void Update ()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        m_Timer += Time.deltaTime;

        if (horizontal == 0f && vertical == 0f)
        {
            m_CurrentPlayerState = PlayerStates.Idle;
        }
        else if(horizontal != 0f || vertical != 0f && m_CurrentPlayerState != PlayerStates.Aim)
        {
            m_CurrentPlayerState = PlayerStates.Walk;
            m_Animator.SetFloat("Speed", vertical);
            transform.RotateAround(transform.position, transform.up, horizontal * RotationSpeed * Time.deltaTime);
            if (vertical != 0f)
            {
                PlaySound(WaterFootSteps, true, true);
            }
        }

        if(Input.GetButton("Aim"))
        {
            m_Animator.SetLayerWeight(1, 1f);
            m_CurrentPlayerState = PlayerStates.Aim;

            StartCoroutine("FindTargetsWithDelay", 0.5f);

            if (m_CurrentPlayerState == PlayerStates.Aim && Input.GetButtonDown("Action"))
            {
                Debug.Log("Fire");

                if (m_Timer >= m_AttackTimer)
                {
                    StartCoroutine("Fire");

					if(OnFire != null) {
						OnFire();
					}
                }
            }
        }

        if(Input.GetButtonUp("Aim"))
        {
            m_Animator.SetLayerWeight(1, 0f);
            m_CurrentPlayerState = PlayerStates.Idle;
            m_CurrentTarget = null;

            StopAllCoroutines();
        }
	}

    IEnumerator Fire()
    {
        if (!m_AudioSource.isPlaying)
        {
            PlaySound(ShotGunShoot, false, true);
        }

        m_MuzzleFlashObject.SetActive(true);

        AmmoCountMax -= AmmoCountNow;
        AmmoCountNow -= 2;
        AmmoCountNow = 2;

        yield return new WaitForSeconds(m_MuzzleFlashTime);

        if (m_CurrentTarget != null)
        {
            m_CurrentTarget.AjustHealth(-m_WeaponDamage);
        }

        m_MuzzleFlashObject.SetActive(false);
        m_Timer = 0f;
    }

    public Vector3 DifFromAngle(float angleInDegres, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegres += transform.eulerAngles.y;
        }

        return new Vector3(Mathf.Sin( angleInDegres * Mathf.Deg2Rad), 0f, Mathf.Cos( angleInDegres * Mathf.Deg2Rad));
    }

    IEnumerator FindTargetsWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        FindTargets();
    }

    public void FindTargets()
    {
        Debug.Log("FindTargets");

        Tartegs.Clear();

        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, ViewRadus, m_TargetMask, QueryTriggerInteraction.Ignore);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Tartegs.Add(targetsInViewRadius[i].transform);

            Transform target = targetsInViewRadius[i].transform;
            Vector3 DirToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, DirToTarget) < ViewAngle / 2)
            {
                float distToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, DirToTarget, distToTarget, m_ObstacleMask, QueryTriggerInteraction.Ignore))
                {
                    target.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.red;
                    m_CurrentTarget = target.GetComponent<Zombie>();
                    m_CurrentTarget.transform.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.red;
                    break;
                }
            }
            else
            {
                if (m_CurrentTarget != null)
                {
                    m_CurrentTarget.transform.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.white;
                    m_CurrentTarget = null;
                }
            }
        }
    }

    public void PlaySound(AudioClip a_Clip, bool a_WaitToEnd, bool a_RandomPitch)
    {
        if(a_RandomPitch)
        {
            System.Random prng = new System.Random();
            m_AudioSource.pitch = (prng.Next(75, 125) * 0.01f);
        }

        if (a_WaitToEnd)
        {
            if (!m_AudioSource.isPlaying)
            {
                m_AudioSource.clip = a_Clip;
                m_AudioSource.Play();
            }  
        }
        else
        {
            m_AudioSource.Stop();
            m_AudioSource.clip = a_Clip;
            m_AudioSource.Play();
        }
    }
}
