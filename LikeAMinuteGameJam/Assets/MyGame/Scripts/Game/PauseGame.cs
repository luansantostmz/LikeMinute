using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
	public GameObject pausePanel;
	bool isPaused;
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape)) 
		{
			isPaused = !pausePanel.activeSelf;
			pausePanel.SetActive(!pausePanel.activeSelf);
		}

		if(isPaused)
		{
			Time.timeScale = 0;
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
		else 
		{
			Time.timeScale = 1;
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
		
	}

	public void Resume() 
	{
		isPaused = !pausePanel.activeSelf;
		pausePanel.SetActive(!pausePanel.activeSelf);
	}

}
