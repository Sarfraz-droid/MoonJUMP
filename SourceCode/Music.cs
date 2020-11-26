using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Music : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		DontDestroyOnLoad (this.gameObject);
		if (SceneManager.GetActiveScene ().buildIndex >= 2) {
			Destroy (this.gameObject);
		}
	}
}
