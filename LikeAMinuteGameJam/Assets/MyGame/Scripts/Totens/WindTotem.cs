using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindTotem : MonoBehaviour
{
	public bool activateTotem;
	public GameObject[] misc;
	private void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.CompareTag("Player") && activateTotem)
		{
			activateTotem = false;
			Debug.Log("O player colidiu com o WindTotem");
			GameEvents.TotensWindCount?.Invoke(1);
			misc[0].gameObject.SetActive(true);
			misc[1].gameObject.SetActive(true);
			misc[2].gameObject.SetActive(true);
		}
	}
}
