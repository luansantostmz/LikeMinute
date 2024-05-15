using UnityEngine;

public class FaceCamera : MonoBehaviour
{
	void Update()
	{
		// Obt�m a dire��o para a c�mera
		Vector3 directionToCamera = Camera.main.transform.position - transform.position;

		// Mant�m a imagem sempre de frente para a c�mera
		transform.forward = -directionToCamera.normalized;
	}
}
