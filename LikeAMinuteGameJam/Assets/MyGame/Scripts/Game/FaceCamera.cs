using UnityEngine;

public class FaceCamera : MonoBehaviour
{
	void Update()
	{
		// Obtém a direção para a câmera
		Vector3 directionToCamera = Camera.main.transform.position - transform.position;

		// Mantém a imagem sempre de frente para a câmera
		transform.forward = -directionToCamera.normalized;
	}
}
