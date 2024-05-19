using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
	[Header("Reset config")]
	public Transform spawnTransform;

	[Header("Gravity")]
	public float gravity;

	[Space]
	[Header("Move config")]
	[SerializeField] float speed;
	[SerializeField] float speedRun;
	[SerializeField] float speedwindRun;

	[Header("Jump config")]
	public float jumpForce = 10f;
	public float jumpCooldown = 1f;
	public bool doubleJumpEnabled = false;
	bool canDoubleJump;

	[Header("Dash config")]
	[SerializeField] float dashForce = 10;
	[SerializeField] float dashDuration = 0.2f;
	[SerializeField] float dashCooldawn;
	public bool isDashing = false;

	[Header("Blink config")]

	[SerializeField] private float blinkTime = 3f;
	[SerializeField] private float blinkDuration = 0.1f;

	[SerializeField] private SkinnedMeshRenderer meshRenderer;
	[SerializeField] private bool isInvulnerable = false;


	[Space]
	[SerializeField]private bool grounded = false;

	Vector3 direction;

	Animator animator;

	LifePlayer lifePlayer;

	private Rigidbody rb;
	private CapsuleCollider col;
	Transform mainCam;

	Vector2 movementInputs = new Vector2();
	bool IsGrounded => Physics.Raycast(transform.position, Vector3.down, .4f, LayerMask.GetMask("Ground"));
	private void Start()
	{
		animator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody>();
		col = GetComponent<CapsuleCollider>();
		meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();

		mainCam = Camera.main.transform;

		meshRenderer.enabled = true;

		lifePlayer = GetComponent<LifePlayer>();


		speed = speedRun;

		ResetTransform();
	}

	private void OnEnable()
	{
		GameEvents.ResetPlayer += ResetTransform;
	}
	private void OnDisable()
	{
		GameEvents.ResetPlayer -= ResetTransform;
	}
	private void Update()
	{
		if(!isDashing) Gravity();


		animator.SetBool("isGrounded", IsGrounded);
		animator.SetFloat("movementVelocity", movementInputs.normalized.magnitude);

		JumpController();
		MovePlayer();

		//Dash
		if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing)
		{
			StartCoroutine(Dash());
		}		
	}
	void JumpController()
	{ 
		if (!Input.GetKeyDown(KeyCode.Space))
			return;

		if (IsGrounded)
			StartCoroutine(Jump());
		else if (canDoubleJump)
			DoubleJump();
	}
	private void MovePlayer()
	{
		movementInputs = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

		direction.Set(movementInputs.x, 0, movementInputs.y);
		direction = Quaternion.Euler(0, mainCam.eulerAngles.y, 0) * direction;
		direction.Normalize();

		Vector3 desiredFoward = Vector3.RotateTowards(transform.forward, direction, 80 * Time.deltaTime, 0);
		rb.MoveRotation(Quaternion.LookRotation(desiredFoward));

		bool running = false;
		if (movementInputs.magnitude != 0)
		{
			running = true;
		}

		//animator.SetBool("run", running);

		rb.MovePosition(rb.position + direction * speed * Time.deltaTime);

	}

	private IEnumerator WindSpeed() 
	{
		speed = speedwindRun;

		yield return new WaitForSeconds(10f);

		speed = speedRun;
	}


	private IEnumerator Jump()
	{		
		rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
		Debug.Log("APENAS UM PULO, NAO SOU MT BOM");

		if (doubleJumpEnabled)
			canDoubleJump = true;

		yield return new WaitForSeconds(.25f);
		yield return new WaitUntil(() => IsGrounded);

		canDoubleJump = false;
		ResetJumpAnimation();
	}
	private void DoubleJump()
	{
		canDoubleJump = false;

		Debug.Log("PULEI DUPLAMENTE, SE FODEU");
		animator.SetBool("doublejump", true);
		animator.SetBool("jump", false);
		rb.velocity = new Vector3(rb.velocity.x, jumpForce * 1.5f, rb.velocity.z);

		Invoke(nameof(ResetJumpAnimation), jumpCooldown);
	}
	private void ResetJumpAnimation()
	{
		animator.SetBool("doublejump", false);
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
	void ResetTransform() 
	{
		transform.position = spawnTransform.position;
	}
	public void TakeDamage()
	{
		if (!isInvulnerable)
		{
			isInvulnerable = true;
			StartCoroutine(InvulnerabilityCoroutine());
		}
	}
	IEnumerator InvulnerabilityCoroutine()
	{
		// Faça o personagem piscar durante 3 segundos		
		int blinkCount = Mathf.FloorToInt(blinkTime / (2 * blinkDuration));

		for (int i = 0; i < blinkCount; i++)
		{
			meshRenderer.enabled = !meshRenderer.enabled;
			yield return new WaitForSeconds(blinkDuration);
		}

		// Garanta que o personagem esteja visível no final
		meshRenderer.enabled = true;

		// Desativar invulnerabilidade após o efeito de piscar
		isInvulnerable = false;
	}
	private void OnTriggerEnter(Collider col)
	{
		if (col.CompareTag("obstacle") && !isInvulnerable)
		{
			lifePlayer.TakeDamage(1);
			TakeDamage();			
		}
		if (col.CompareTag("heart"))
		{
			lifePlayer.AddHealth(1);
			Destroy(col.gameObject);
		}
		if (col.CompareTag("coin"))
		{
			GameEvents.TimeCoin?.Invoke(2);
			Destroy(col.gameObject);
		}
		if (col.CompareTag("forceWind")) 
		{
			StartCoroutine(WindSpeed());
		}
		if (col.CompareTag("FragmentMemory")) 
		{
			GameEvents.FragmentMemory?.Invoke();
			lifePlayer.AddHealth(3);
			GameEvents.TimeCoin?.Invoke(10);
			Destroy(col.gameObject);
		}
	}


}
