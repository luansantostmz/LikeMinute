using UnityEngine;
using TMPro;
using System.Collections;

public class UiController : MonoBehaviour
{
	public float tempoTotal = 60f;
	private float tempoAtual;
	public TextMeshProUGUI textoContador;

	public GameObject[] fragments;
	int fragmentsCount;

	private Coroutine contadorCoroutine;

	void Start()
	{
		RestartPlayerPosition();
	}

	private void OnEnable()
	{
		GameEvents.OnExitSafeZone += IniciarContador;
		GameEvents.TimeCoin += AddCoinTime;
		GameEvents.FragmentMemory += AddFragmentMemory;
	}

	private void OnDisable()
	{
		GameEvents.OnExitSafeZone -= IniciarContador;
		GameEvents.TimeCoin -= AddCoinTime;
		GameEvents.FragmentMemory -= AddFragmentMemory;
	}

	void IniciarContador()
	{
		if (contadorCoroutine != null)
		{
			StopCoroutine(contadorCoroutine);
		}
		tempoAtual = tempoTotal;
		textoContador.transform.parent.gameObject.SetActive(true);
		contadorCoroutine = StartCoroutine(AtualizarContador());
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

	public void AddCoinTime(int value)
	{
		tempoAtual += value;
		textoContador.text = tempoAtual.ToString("F0");
	}

	public void AddFragmentMemory()
	{
		fragmentsCount++;
		if (fragmentsCount == 1) fragments[0].SetActive(true);
		if (fragmentsCount == 2) fragments[1].SetActive(true);
		if (fragmentsCount == 3) fragments[2].SetActive(true);
		if (fragmentsCount >= 3) fragmentsCount = 3;
	}

	public void RestartPlayerPosition()
	{
		if (contadorCoroutine != null)
		{
			StopCoroutine(contadorCoroutine);
			contadorCoroutine = null;
		}
		GameEvents.ResetPlayer?.Invoke();
		textoContador.transform.parent.gameObject.SetActive(false);
	}
}
