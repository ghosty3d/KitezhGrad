using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rigidbody;
    public Animator animator;
    public float MoveSpeed = 10f;
    public float RotationSpeed = 50f;

    private float horizontal;
    private float vertical;

	void Start () 
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        animator.SetFloat("Speed", vertical);
        //animator.SetFloat("Direction", horizontal);
        transform.RotateAround(transform.position, transform.up, horizontal * RotationSpeed * Time.deltaTime);
	}
}
