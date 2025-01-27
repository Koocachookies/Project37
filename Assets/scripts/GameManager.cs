﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class GameManager : MonoBehaviour 
{
	private InputManager inputManager;
	public GameObject canvasPause;
	public GameObject canvasInventory;
	public GameObject canvasMap;
	public GameObject canvasInv;

	EventSystem eventSystem;
	public GameObject pauseButton;
	public GameObject invButton;
	public GameObject mapButton;

	public GameObject inventory;
	public GameObject map;
	public GameObject notes;
	
	private void Awake () 
	{
		inputManager = GetComponent<InputManager>();
		eventSystem = EventSystem.current;
	}
	
	public void Pause()
	{
		inputManager.SetPause(true);
		Time.timeScale = 0;
		canvasPause.SetActive(true);
		eventSystem.SetSelectedGameObject(pauseButton);
	}

	public void Resume()
	{
		inputManager.SetPause(false);
		Time.timeScale = 1;
		canvasPause.SetActive(false);
	}

	public void OpenInventory()
	{
		inputManager.SetInventory(true);
		canvasInv.SetActive(true);
		Time.timeScale = 0;
		canvasInventory.SetActive(true);
		eventSystem.SetSelectedGameObject(invButton);
	}

	public void CloseInventory()
	{
		inputManager.SetInventory(false);
		canvasInv.SetActive(false);
		Time.timeScale = 1;
		//canvasInventory.SetActive(false);
		//inventory.SetActive(true);
		map.SetActive(false);
		notes.SetActive(false);
	}
	
	public void OpenMap()
	{
		inputManager.SetMap(true);
		Time.timeScale = 0;
		canvasMap.SetActive(true);
		canvasInv.SetActive(false);
		canvasInventory.SetActive(true);
		eventSystem.SetSelectedGameObject(mapButton);
	}

	public void CloseMap()
	{
		inputManager.SetMap(false);
		Time.timeScale = 1;
		canvasMap.SetActive(false);
		//canvasInventory.SetActive(false);
		//inventory.SetActive(true);
		map.SetActive(false);
		notes.SetActive(false);
	}
}
