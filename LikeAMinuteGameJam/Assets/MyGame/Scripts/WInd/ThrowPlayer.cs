using UnityEngine;

public class ThrowAndFall : MonoBehaviour
{
	public float throwForce;
	public float maxUpwardForce;
	public Vector3 throwDirection;

	private bool isThrown = false;

	private void OnTriggerEnter(Collider other)
	{
		
		if (other.CompareTag("Player"))
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
