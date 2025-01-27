﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //poner esto en todo lo que vaya a cambiar de escena
using UnityEngine.UI;


public class ChangeScene : MonoBehaviour 
{
	public int scene; //se introduce la scena a la que se quiere ir
	private bool isInsideTrigger = false;

	public Image black;
	public Animator animator;

	void Update()
	{
		if (isInsideTrigger && Input.GetButtonDown("Action"))
		{
			Debug.Log ("Change Scene");
			//SceneManager.LoadScene(scene); //linea que hace que funcione
			//Fade();
			StartCoroutine(Fade());
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			isInsideTrigger = true;
		}
	}
	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			isInsideTrigger = false;
		}
	}
	//Change Scene In Menu
	/*public void ChangeToScene (int num)
	{
		SceneManager.LoadScene(num);
	}*/
	public void FadeChangeScene(int num)
	{
		StartCoroutine(FadeButton(num));
	}
	public void Death()
	{
		StartCoroutine(FadeDead());
	}
	public void ExitGame()
	{
		Debug.Log("Exit Game");
		Application.Quit();
	}
	/*public void*/ IEnumerator Fade()
	{
		//animator.SetTrigger("FadeOut");
		//SceneManager.LoadScene(scene);
		animator.SetBool("Fade", true);
		yield return new WaitUntil(()=>black.color.a==1);
		SceneManager.LoadScene(scene);
	}
	IEnumerator FadeButton(int num)
	{
		animator.SetBool("Fade", true);
		yield return new WaitUntil(()=>black.color.a==1);
		SceneManager.LoadScene(num);
	}
	IEnumerator FadeDead()
	{
		//animator.SetTrigger("FadeOut");
		//SceneManager.LoadScene(scene);
		animator.SetBool("Fade", true);
		yield return new WaitUntil(()=>black.color.a==1);
		SceneManager.LoadScene(4);
	}
}
