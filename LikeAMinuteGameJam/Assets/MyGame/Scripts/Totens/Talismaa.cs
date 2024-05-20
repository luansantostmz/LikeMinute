using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talismaa : MonoBehaviour
{	
	private void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.CompareTag("Player"))
		{
			GameEvents.Talisma?.Invoke(1);
		}
	}
}
