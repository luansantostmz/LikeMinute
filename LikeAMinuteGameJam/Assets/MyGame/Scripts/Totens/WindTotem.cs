using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindTotem : MonoBehaviour
{
	public bool activateTotem;
	private void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.CompareTag("Player") && activateTotem)
		{
			activateTotem = false;
			Debug.Log("O player colidiu com o WindTotem");
			GameEvents.TotensWindCount?.Invoke();			
		}
	}
}
