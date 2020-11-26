using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_Manager : MonoBehaviour {

	public Animator transition;
	public void Back()
	{
		StartCoroutine (Back_process ());
	}
	IEnumerator Back_process()
	{
		transition.SetBool ("End", true);
		yield return new WaitForSeconds (1f);
		SceneManager.LoadScene (0);
	}
	public void Level_Start(int l)
	{
		StartCoroutine (Level (l+1));
	}
	IEnumerator Level(int t)
	{
		transition.SetBool ("End", true);
		yield return new WaitForSeconds (1f);
		SceneManager.LoadScene (t);
	}
}
