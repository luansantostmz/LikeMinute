using UnityEngine;

public class Resolucao : MonoBehaviour
{
	void Start()
	{
		// Defina a resolução desejada
		Screen.SetResolution(1920, 1080, true); // Os três parâmetros são largura, altura e tela cheia (true para tela cheia, false caso contrário)
	}
}
