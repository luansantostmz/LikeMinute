using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class DoorWind : MonoBehaviour
{

	public int totemPorta1Count;
	public int totemPorta2Count;
	public int totemPorta3Count;
	public int totemPorta4Count;
	public int totemPorta5Count;

	public int totalPorta1;
	public int totalPorta2;
	public int totalPorta3;
	public int totalPorta4;
	public int totalPorta5;

	public GameObject door1;
	public GameObject door2;
	public GameObject door3;
	public GameObject door4;
	public GameObject door5;	

	private void OnEnable()
	{
		GameEvents.TotemPorta1 += AddPorta1;
		GameEvents.TotemPorta2 += AddPorta2;
		GameEvents.TotemPorta3 += AddPorta3;
		GameEvents.TotemPorta4 += AddPorta4;
		GameEvents.TotemPorta5 += AddPorta5;
	}
	private void OnDisable()
	{
		GameEvents.TotemPorta1 -= AddPorta1;
		GameEvents.TotemPorta2 -= AddPorta2;
		GameEvents.TotemPorta3 -= AddPorta3;
		GameEvents.TotemPorta4 -= AddPorta4;
		GameEvents.TotemPorta5 -= AddPorta5;
	}

	private void Update()
	{
		if(totemPorta1Count == totalPorta1) 
		{
			door1.SetActive(true);
		}
		if (totemPorta2Count == totalPorta2)
		{
			door1.SetActive(true);
		}
		if (totemPorta3Count == totalPorta3)
		{
			door1.SetActive(true);
		}
		if (totemPorta4Count == totalPorta4)
		{
			door1.SetActive(true);
		}
		if (totemPorta5Count == totalPorta5)
		{
			door1.SetActive(true);
		}
	}

	public void AddPorta1(int value) 
	{
		totemPorta1Count+= value;
	}
	public void AddPorta2(int value)
	{
		totemPorta2Count += value;
	}
	public void AddPorta3(int value)
	{
		totemPorta3Count += value;
	}
	public void AddPorta4(int value)
	{
		totemPorta4Count += value;
	}
	public void AddPorta5(int value)
	{
		totemPorta5Count += value;
	}


}
