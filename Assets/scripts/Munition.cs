﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Munition : MonoBehaviour 
{
	private Gun weapon;
	public GameObject ammoObject;
	private bool isInsideTrigger = false;
	public int munition;

	public GameObject TextPanel = null;
	public string message = "Hello World";
	public Text eText;
	private bool MessageReaded = false;

	private SoundPlayer sound;

	// Use this for initialization
	void Start () 
	{
		weapon = GameObject.FindGameObjectWithTag("Weapon").GetComponent<Gun>();
		sound = GetComponentInChildren<SoundPlayer>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(isInsideTrigger && Input.GetButtonDown("Action"))
		{
			//weapon.GetAmmo(munition);
			//ammoObject.SetActive(false);
			if(MessageReaded)
			{
				ReadEnd();
			}
			else 
			{
				Read();
			}
		}
		
	}
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") //solo funciona con player
		{
			isInsideTrigger = true; //cambia el bool
		}
	}
	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			isInsideTrigger = false;
		}
	}
	private void Read()
	{
		TextPanel.SetActive(true);
		eText.text = message;
		Debug.Log("reading");
		MessageReaded = true;
		Time.timeScale = 0;
		weapon.GetAmmo(munition);
		sound.Play(1, 2);
	}
	private void ReadEnd()
	{
		TextPanel.SetActive(false);
		Debug.Log("quit");
		MessageReaded = false;
		Time.timeScale = 1;
		ammoObject.SetActive(false);
	}
}
