﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointRotate : MonoBehaviour 
{
	private CharacterController controller;

	public enum RotationAxes {MouseXAndY = 0, MouseX = 1, MouseY = 2}
	public RotationAxes axes = RotationAxes.MouseXAndY;
	public float sensitivityX = 15f;
	//public float sensitivityY = 15f;

	public float minimumX = -360f;
	public float maximumX = 360f;
	float rotationY = 0f;
	// Use this for initialization
	void Start () 
	{
		controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (axes == RotationAxes.MouseXAndY)
		{
			float rotationX = transform.localEulerAngles.y + Input.GetAxis("Horizontal") * sensitivityX;

			//rotationY +=  Input.GetAxis("Vertical") * sensitivityY;
			//rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

			transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
		}
		else if (axes == RotationAxes.MouseX){
			transform.Rotate(0, Input.GetAxis("Horizontal") * sensitivityX, 0);
		}
		else
		{
			//rotationY += Input.GetAxis("Vertical") * sensitivityY;
			//rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

			transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
		}
	}
}
