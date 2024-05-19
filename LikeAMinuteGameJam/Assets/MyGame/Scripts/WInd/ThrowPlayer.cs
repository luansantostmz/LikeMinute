using UnityEngine;

public class ThrowAndFall : MonoBehaviour
{
	public float throwForce;
	public float maxUpwardForce;
	public Vector3 throwDirection;
	public bool DisableGravity;

	private bool isThrown = false;

	PlayerController pController;

	private void OnTriggerEnter(Collider other)
	{
		pController = other.GetComponent<PlayerController>();

		if (other.CompareTag("Player") && !pController.isDashing)
		{
			if (DisableGravity)
				other.GetComponent<PlayerController>().IsGravityEnabled = false;
			
			Rigidbody rb = other.GetComponent<Rigidbody>();
			
			if (rb == null)
			{
				rb = other.gameObject.AddComponent<Rigidbody>();
			}
			
			throwDirection.Normalize();
			
			float totalForce = throwForce;
			
			if (throwDirection == Vector3.up && throwForce > maxUpwardForce)
			{
				totalForce = maxUpwardForce;
			}
			
			rb.AddForce(throwDirection * totalForce, ForceMode.Impulse);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player") && DisableGravity)
		{
			other.GetComponent<PlayerController>().IsGravityEnabled = true;
		}
	}
}
