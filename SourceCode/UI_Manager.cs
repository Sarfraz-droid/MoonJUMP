using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UI_Manager : MonoBehaviour {

	[Header("Transition : ")]
	public Animator transition;
	public void TryAgain()
	{
		StartCoroutine (TryAgain_start ());
	}
	public void MainMenu()
	{
		StartCoroutine (MainMenu_Start ());
	}
	public void NextLevel()
	{
		StartCoroutine (NextLevel_Process ());
	}
	IEnumerator NextLevel_Process()
	{
		if (SceneManager.GetActiveScene ().buildIndex != 11) {
			transition.SetBool ("End", true);
			yield return new WaitForSeconds (1f);
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
		} else {
			transition.SetBool ("End", true);
			yield return new WaitForSeconds (1f);
			SceneManager.LoadScene (0);
		}
			
	}
	IEnumerator MainMenu_Start()
	{
		transition.SetBool ("End", true);
		yield return new WaitForSeconds (1f);
		SceneManager.LoadScene (0);
	}
	IEnumerator TryAgain_start()
	{
		transition.SetBool ("End", true);
		yield return new WaitForSeconds (1f);
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
	}
}
