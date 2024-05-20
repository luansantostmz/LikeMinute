using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class DoorWind : MonoBehaviour
{

	public int totemPorta1Count;
	public int talismaCount;
	public int platformCount;

	public int totalPorta1;
	public int totalTalisma;
	public int totalplatform;

	public GameObject door1;
	public GameObject doorGameEnd;
	public GameObject platform;

	private void OnEnable()
	{
		GameEvents.TotemPorta1 += AddPorta1;
		GameEvents.Talisma += AddTalisma;
		GameEvents.Platform += AddPlatform;
	}
	private void OnDisable()
	{
		GameEvents.TotemPorta1 -= AddPorta1;
		GameEvents.Talisma -= AddTalisma;
		GameEvents.Platform -= AddPlatform;
	}

	private void Update()
	{
		if(totemPorta1Count == totalPorta1) 
		{
			door1.SetActive(false);
		}
		if (talismaCount == totalTalisma)
		{
			doorGameEnd.SetActive(false);
		}
		if (platformCount == totalplatform)
		{
			platform.SetActive(true);
		}
	}

	public void AddPorta1(int value) 
	{
		totemPorta1Count+= value;
	}
	public void AddTalisma(int value)
	{
		talismaCount += value;
	}
	public void AddPlatform(int value)
	{
		platformCount += value;
	}
}
