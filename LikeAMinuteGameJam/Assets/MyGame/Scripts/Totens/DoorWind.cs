using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorWind : MonoBehaviour
{

	public int windTotemCount;
	public GameObject door;
	public GameObject wind;

	public bool doorOrWind;

	private void OnEnable()
	{
		GameEvents.TotensWindCount += AddWindCount;		
	}
	private void OnDisable()
	{
		GameEvents.TotensWindCount -= AddWindCount;
	}

	private void Update()
	{
		if(windTotemCount == 1) 
		{
			if (doorOrWind) 
			{
				door.SetActive(true);
			}
			else
			{
				wind.SetActive(true);
			}
			
		}
	}

	public void AddWindCount() 
	{
		windTotemCount+= 1;
	}


}
