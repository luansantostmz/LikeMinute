using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class PlayerMove : MonoBehaviour
{
	[Header("Gravity")]
	public float gravity;

	[Header("Move config")]
	[SerializeField] float speedWalk;
	[SerializeField] float speedRun;

	[Header("Jump config")]
	public float jumpForce = 10f;
	public float jumpCooldown = 1f;
	public int jumpsLeft;
	public bool doubleJumpEnabled = false;
	[SerializeField]private bool canJump = true;

	[Header("Dash config")]
	[SerializeField] float dashForce = 10;
	[SerializeField] float dashDuration = 0.2f;
	[SerializeField] float dashCooldawn;
	[SerializeField] bool isDashing = false;

	[SerializeField]private bool grounded = false;

	Vector3 direction;

	Animator animator;



	private Rigidbody rb;
	private CapsuleCollider col;

	Transform mainCam;
	private void Start()
	{
		animator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody>();
		col = GetComponent<CapsuleCollider>();

		mainCam = Camera.main.transform;
	}

	private void Update()
	{
		Gravity();		
		if(jumpsLeft <= 0 ) Invoke("ResetDoubleJump", 1);
		//Jump
		RaycastHit hit;
		grounded = Physics.SphereCast(col.bounds.center, col.radius * 0.9f, Vector3.down, out hit,1);

		if (doubleJumpEnabled) 
		{

			if (grounded && canJump && Input.GetKeyDown(KeyCode.Space))
			{				
				Jump();
				jumpsLeft--;
			}
			else if (Input.GetKeyDown(KeyCode.Space) && jumpsLeft == 1 && canJump)
			{
				Jump();
				jumpsLeft--;				
			}
		}
		else 
		{
			if (grounded && canJump && Input.GetKeyDown(KeyCode.Space))
			{				
				Jump();				
			}
		}

				

		//Dash
		if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing)
		{
			StartCoroutine(Dash());
		}

	}
	private void FixedUpdate()
	{
		float horizontal = Input.GetAxisRaw("Horizontal");
		float vertical = Input.GetAxisRaw("Vertical");

		direction.Set(horizontal, 0, vertical);
		direction = Quaternion.Euler(0, mainCam.eulerAngles.y, 0) * direction;
		direction.Normalize();

		Vector3 desiredFoward = Vector3.RotateTowards(transform.forward, direction, 20 * Time.deltaTime, 0);
		rb.MoveRotation(Quaternion.LookRotation(desiredFoward));

		bool running = false;
		if (horizontal != 0 || vertical != 0)
		{
			running = true;
		}

		animator.SetBool("run", running);

		rb.MovePosition(rb.position + direction * speedWalk * Time.deltaTime);

	}
	private void Jump()
	{		
		animator.SetBool("jump", true);
		rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z); 
		rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
		canJump = false;

		Invoke("ResetJump", jumpCooldown);
	}
	private void ResetJump()
	{
		canJump = true;
		animator.SetBool("jump", false);
	}
	private void ResetDoubleJump()
	{
		jumpsLeft = 2;
	}

	IEnumerator Dash()
	{
		animator.SetBool("dash", true);
		isDashing = true;
		rb.velocity = transform.forward * dashForce; 
		yield return new WaitForSeconds(dashDuration);
		rb.velocity = Vector3.zero;		
		animator.SetBool("dash", false);
		yield return new WaitForSeconds(dashCooldawn);
		isDashing = false;
	}


	public void Gravity() 
	{
		Vector3 gravityVector = new Vector3(0, -gravity * rb.mass, 0);
		rb.AddForce(gravityVector, ForceMode.Acceleration);
	}
}
