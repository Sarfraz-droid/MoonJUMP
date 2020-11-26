using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Manager : MonoBehaviour {

	public Animator transition;
	public void Level_Menu_Start()
	{
		StartCoroutine (Start_menu ());
	}
	IEnumerator Start_menu()
	{
		transition.SetBool ("End", true);
		yield return new WaitForSeconds (1f);
		SceneManager.LoadScene (1);
	}
	public void Game_Exit()
	{
		StartCoroutine (ExiT_Process ());
	}
	IEnumerator ExiT_Process()
	{
		transition.SetBool ("End", true);
		yield return new WaitForSeconds (1f);
		Application.Quit ();
	}

}
