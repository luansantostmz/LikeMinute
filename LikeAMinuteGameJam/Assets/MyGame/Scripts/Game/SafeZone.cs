using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZone : MonoBehaviour
{
	private void OnTriggerExit(Collider col)
	{
		if (col.gameObject.CompareTag("Player"))
		{
			Debug.Log("O player saiu da safezone");
			GameEvents.OnExitSafeZone?.Invoke();
		}		
	}
}
