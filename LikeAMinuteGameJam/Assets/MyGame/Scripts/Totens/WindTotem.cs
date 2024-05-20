using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindTotem : MonoBehaviour
{
	public string newTag;

	private void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.CompareTag("Player") && gameObject.tag != newTag)
		{
			GameEvents.TotemPorta1?.Invoke(1);
			this.gameObject.tag = newTag;
		}
	}
}
