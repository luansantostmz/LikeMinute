using UnityEngine;
using TMPro;
using System.Collections;

public class Timer : MonoBehaviour
{
	public float tempoTotal = 60f;
	private float tempoAtual; 
	public TextMeshProUGUI textoContador;	

	void Start()
	{
		tempoAtual = tempoTotal;		
	}
	private void OnEnable()
	{
		GameEvents.OnExitSafeZone+= IniciarContador; 
	}
	private void OnDisable()
	{
		GameEvents.OnExitSafeZone -= IniciarContador;
	}
	void IniciarContador()
	{
		StartCoroutine(AtualizarContador());
	}

	IEnumerator AtualizarContador()
	{
		while (tempoAtual > 0f)
		{
			textoContador.text = tempoAtual.ToString("F0"); 
															
			yield return new WaitForSeconds(1f);
			tempoAtual--; 
		}

		RestartPlayerPosition();
	}

	public void RestartPlayerPosition() 
	{
		GameEvents.ResetPlayer?.Invoke();
		tempoAtual = tempoTotal;
	}
}
