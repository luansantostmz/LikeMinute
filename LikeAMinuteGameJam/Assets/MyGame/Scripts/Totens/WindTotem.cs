using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindTotem : MonoBehaviour
{
	public string newTag = "defautl";
	private void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.CompareTag("Player"))
		{
			this.gameObject.tag = newTag;
		}
	}
}
