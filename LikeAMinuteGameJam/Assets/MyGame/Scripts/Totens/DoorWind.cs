using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorWind : MonoBehaviour
{

	public int windTotemCount;
	public int totalTotemWInd;
	public GameObject door;
	public GameObject wind;

	public bool doorObject;

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
		if(windTotemCount == totalTotemWInd) 
		{
			if (doorObject) 
			{
				door.SetActive(true);
			}
			else
			{
				wind.SetActive(true);
			}
			
		}
	}

	public void AddWindCount(int value) 
	{
		windTotemCount+= value;
	}


}
