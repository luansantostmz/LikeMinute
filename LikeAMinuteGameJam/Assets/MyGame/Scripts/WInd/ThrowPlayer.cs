using UnityEngine;

public class ThrowAndFall : MonoBehaviour
{
	public float throwForce;
	public float maxUpwardForce;
	public Vector3 throwDirection;

	private bool isThrown = false;

	PlayerController pController;

	private void OnTriggerEnter(Collider other)
	{
		pController = other.GetComponent<PlayerController>();

		if (other.CompareTag("Player") && !pController.isDashing)
		{
			
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
}
