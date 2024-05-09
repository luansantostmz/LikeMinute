using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{

	void Start()
	{		
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}
}
