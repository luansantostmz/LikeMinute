using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePlayer : MonoBehaviour
{
	private int maxHealth;
	private int currentHealth;
	public GameObject[] hearts;

	public Timer timer;

	void Start()
	{
		maxHealth = hearts.Length;
		currentHealth = maxHealth;

		UpdateHearts();
	}
	public void TakeDamage(int amount)
	{
		currentHealth -= amount;
		currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

		UpdateHearts();

		if (currentHealth <= 0)
		{			
			timer.RestartPlayerPosition();
			AddHealth(3);
		}
	}
	public void AddHealth(int amount)
	{
		currentHealth += amount;
		currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

		UpdateHearts();
	}
	private void UpdateHearts()
	{
		for (int i = 0; i < maxHealth; i++)
		{
			if (i < currentHealth)
			{
				hearts[i].SetActive(true);
			}
			else
			{
				hearts[i].SetActive(false);
			}
		}
	}
}
