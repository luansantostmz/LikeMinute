using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;



public class InteractableObject : MonoBehaviour
{
	public GameObject interactionIcon;
	public float interctableDistance;
	private Transform player;
	public TextMeshProUGUI text;
	public string textcontent;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;

	}

    private void OnTriggerEnter(Collider other)
    {
		if (other.CompareTag("Player"))
		{
            interactionIcon.SetActive(true);

            text.text = textcontent;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactionIcon.SetActive(false);

        }
    }
}
