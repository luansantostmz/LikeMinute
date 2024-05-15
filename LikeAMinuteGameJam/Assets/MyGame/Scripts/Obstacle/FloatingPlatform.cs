using UnityEngine;

public class FloatingPlatform : MonoBehaviour
{
	public float moveSpeed = 3f; // Velocidade de movimento da plataforma
	public float moveDistance = 5f; // Distância total a percorrer
	private float originalPositionX; // Posição original da plataforma
	private float moveDirection = 1f; // Direção de movimento (1 para direita, -1 para esquerda)

	void Start()
	{
		originalPositionX = transform.position.x;
	}

	void Update()
	{
		// Calcula a nova posição da plataforma
		float newPositionX = transform.position.x + moveDirection * moveSpeed * Time.deltaTime;

		// Verifica se a plataforma chegou ao final da distância
		if (Mathf.Abs(newPositionX - originalPositionX) >= moveDistance)
		{
			// Se chegou ao final, muda a direção de movimento
			moveDirection *= -1f;
		}

		// Move a plataforma para a nova posição
		transform.position = new Vector3(newPositionX, transform.position.y, transform.position.z);
	}
}
