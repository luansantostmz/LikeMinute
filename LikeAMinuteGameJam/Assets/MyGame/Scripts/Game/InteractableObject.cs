using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
	public GameObject interactionIcon;
	public float interctableDistance;
	private Transform player;

	private AudioSource audioSource;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
		audioSource = GetComponent<AudioSource>();
	}

	void Update()
	{
		float distance = Vector3.Distance(transform.position, player.position);
		Debug.Log(distance);
		if(distance <= interctableDistance) 
		{ 
			interactionIcon.SetActive(true); 

			if(Input.GetKeyDown(KeyCode.E)) 
			{
				audioSource.Play();
			}		
		}
		else { interactionIcon.SetActive(false); }
	}
}
