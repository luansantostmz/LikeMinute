using System.Collections;
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
	public float doubleJumpForce = 10f;
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

	[SerializeField] private MeshRenderer meshRenderer;
	[SerializeField] private bool isInvulnerable = false;


	[Space]
	[SerializeField] PlayerSFX SFX;
	[SerializeField]private bool grounded = false;

	Vector3 direction;

	Animator animator;

	LifePlayer lifePlayer;

	private Rigidbody rb;
	private CapsuleCollider col;
	Transform mainCam;

	public bool IsGravityEnabled { get; set; } = true;

	Vector2 movementInputs = new Vector2();
	bool IsGrounded => Physics.Raycast(transform.position, Vector3.down, .4f, LayerMask.GetMask("Ground"));
	private void Start()
	{
		animator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody>();
		col = GetComponent<CapsuleCollider>();
		meshRenderer = GetComponentInChildren<MeshRenderer>();

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
		AudioManager.Play(SFX.Jump);
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
		AudioManager.Play(SFX.Jump);
		canDoubleJump = false;

		Debug.Log("PULEI DUPLAMENTE, SE FODEU");
		animator.SetBool("doublejump", true);
		animator.SetBool("jump", false);
		rb.velocity = new Vector3(rb.velocity.x, doubleJumpForce, rb.velocity.z);

		Invoke(nameof(ResetJumpAnimation), jumpCooldown);
	}
	private void ResetJumpAnimation()
	{
		animator.SetBool("doublejump", false);
	}
	IEnumerator Dash()
	{
		AudioManager.Play(SFX.Dash);
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
		if (!IsGravityEnabled)
		{
			rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
			return;
		}

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
			AudioManager.Play(SFX.Damage);
			lifePlayer.TakeDamage(1);
			TakeDamage();
		}
		else if (col.CompareTag("heart"))
		{
			AudioManager.Play(SFX.Collect);
			lifePlayer.AddHealth(1);
			Destroy(col.gameObject);
		}
		else if (col.CompareTag("coin"))
		{
			AudioManager.Play(SFX.Collect);
			GameEvents.TimeCoin?.Invoke(2);
			Destroy(col.gameObject);
		}
		else if (col.CompareTag("forceWind"))
		{
			AudioManager.Play(SFX.Collect);
			StartCoroutine(WindSpeed());
		}
		else if (col.CompareTag("FragmentMemory"))
		{
			AudioManager.Play(SFX.Collect);
			GameEvents.FragmentMemory?.Invoke();
			lifePlayer.AddHealth(3);
			GameEvents.TimeCoin?.Invoke(10);
			Destroy(col.gameObject);
		}
		else if (col.CompareTag("ground") && IsGrounded)
		{
			AudioManager.Play(SFX.Land);
		}
	}

	[System.Serializable]
	public class PlayerSFX
	{
		public AudioClip Jump;
		public AudioClip Dash;
		public AudioClip Collect;
		public AudioClip Land;
		public AudioClip Interaction;
		public AudioClip Damage;
	}
}
