﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InputManager : MonoBehaviour
{
    private NavMeshAgent myNav;
	private Rigidbody rigidbody;

	private GameManager gameManager;
	public GameObject cameraGod;
    private Gun gun;
	private PlayerBehaviour plBehaviour;
	private TankControls2 tankControl;
	private EnableDisable enDis;
	public int damage;
	private int cure = 1;

    private MouseCursor mouseCursor;
	public bool isPaused = false;
	public bool isInventoryOpened = false;
	public bool isMapOpened = false;
	private bool godActive = false;

	public bool canShoot;
	private SoundPlayer sound;

	// Use this for initialization
	void Start ()
    {
		//shoot2 = GetComponent<Animator>();
		gameManager = GetComponent<GameManager>();
        myNav = GameObject.FindGameObjectWithTag("Player").GetComponent<NavMeshAgent>();
		rigidbody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        gun = GameObject.FindGameObjectWithTag("Weapon").GetComponent<Gun>();
		plBehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
		tankControl = GameObject.FindGameObjectWithTag("Player").GetComponent<TankControls2>();
		enDis = GameObject.FindGameObjectWithTag("Player").GetComponent<EnableDisable>();

        mouseCursor = new MouseCursor();
        mouseCursor.HideCursor();

		sound = GetComponentInChildren<SoundPlayer>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		if(gun.isShooting)
		{
			canShoot = false;
		}
		if(!gun.isShooting)
		{
			canShoot = true;
		}

		if(gun.currentAmmo <= 0)
		{
			canShoot = false;
		}
	}

	void Update ()
    {
        if(Input.GetMouseButtonDown(0)) mouseCursor.HideCursor();
		
        else if(Input.GetKeyDown(KeyCode.Escape)) mouseCursor.ShowCursor();

		

		if (Input.GetButtonDown("Fire") && enDis.isPointing && enDis.precisionActive == false && canShoot == true) 
		{
			gun.Shot ();
			//Debug.Log("Shoot");
			enDis.timeCounter = 0;
			sound.Play(1, 2);
		}
		else if(Input.GetButtonDown("Fire") && enDis.isPointing && enDis.precisionActive && canShoot) 
		{
			gun.PrecisionShot ();
			//Debug.Log("SpecialShoot");
			enDis.timeCounter = 0;
			enDis.precisionActive = false;
			enDis.NormalColor();
			sound.Play(1, 2);
		}
		if((Input.GetAxisRaw("Fire") != 0))
		{
			if(enDis.isPointing && enDis.precisionActive == false && canShoot)
			{
				gun.Shot ();
				//Debug.Log("Shoot");
				enDis.timeCounter = 0;
				sound.Play(1, 2);
			}
			else if(enDis.isPointing && enDis.precisionActive && canShoot)
			{
				gun.PrecisionShot ();
				//Debug.Log("SpecialShoot");
				enDis.timeCounter = 0;
				enDis.precisionActive = false;
				enDis.NormalColor();
				sound.Play(1, 2);
			}
		}
        if(Input.GetButtonDown("Run") /*&& gun.currentAmmo <= 0*/ && enDis.isPointing)
		{
			gun.Reload();
			Debug.Log("reload");
		}




		//            TEST             //
		//QuitarseVida
		if(Input.GetKeyDown(KeyCode.Q)) 
		{
			plBehaviour.Damage(damage);
		}
		if(Input.GetKeyDown(KeyCode.E)) 
		{
			plBehaviour.Curation(cure);
		}
		if(Input.GetKeyDown(KeyCode.F10)) 
		{
			if(!godActive)
			{
				SetGod();
			}
			else
			{
				SetNormal();
			}
		}

		//PAUSE
		if (Input.GetButtonDown("Esc"))
		{
			if(!isPaused && !isInventoryOpened && !isMapOpened)
			{
				gameManager.Pause();
			}
			else if(isInventoryOpened)
			{
				return;
			}
			else if(isMapOpened)
			{
				return;
			}
			else
			{
				gameManager.Resume();
			}
		}

		//INVENTORY
		if (Input.GetButtonDown("Inv"))
		{
			if(!isPaused && !isInventoryOpened && !isMapOpened)
			{
				gameManager.OpenInventory();
				//Debug.Log ("Z pressed");
				//mouseCursor.ShowCursor();
			}
			else if(isPaused)
			{
				return;
			}
			else if(isMapOpened)
			{
				return;
			}
			else
			{
				gameManager.CloseInventory();
				//mouseCursor.HideCursor();
			}
		}
		//MAP
		if (Input.GetButtonDown("Map"))
		{
			if(!isPaused && !isInventoryOpened && !isMapOpened)
			{
				gameManager.OpenMap();
				Debug.Log ("map pressed");
				//mouseCursor.ShowCursor();
			}
			else if(isPaused)
			{
				return;
			}
			else if(isInventoryOpened)
			{
				return;
			}
			else
			{
				gameManager.CloseMap();
				//mouseCursor.HideCursor();
			}
		}
    }

	public void SetPause (bool p)
	{
		isPaused = p;
	}

	public void SetInventory (bool i)
	{
		isInventoryOpened = i;
	}

	public void SetMap (bool m)
	{
		isMapOpened = m;
	}
	public void SetGod()
	{
		plBehaviour.GodMode();
		gun.GodMode();
		tankControl.godMode = true;
		myNav.enabled = false;
		rigidbody.useGravity = false;
		cameraGod.SetActive(true);
		godActive = true;
	}
	public void SetNormal()
	{
		tankControl.godMode = false;
		myNav.enabled = true;
		rigidbody.useGravity = true;
		cameraGod.SetActive(false);
		godActive = false;
	}
}
