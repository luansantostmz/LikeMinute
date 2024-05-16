using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WindForce : MonoBehaviour
{

    public GameObject mesh;

	public float timeToRespawn;
	
	public bool isActive = true;


	private void OnTriggerEnter(Collider col)
	{
		if (col.CompareTag("Player") && isActive) 
		{			
			StartCoroutine(ActiveOrDesactiveMesh());			
		}
	}


	IEnumerator ActiveOrDesactiveMesh() 
	{
		isActive = false;
		mesh.SetActive(false);
		Debug.Log("Sumiu objeto");
		yield return new WaitForSeconds(timeToRespawn);
		Debug.Log("Aparece objeto");
		mesh.SetActive(true);
		isActive = true;
	}
}
