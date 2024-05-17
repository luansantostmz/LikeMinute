using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
	public float speedRotation;

	private void Update()
	{
		transform.Rotate(0,0,speedRotation * Time.deltaTime);
	}
}

