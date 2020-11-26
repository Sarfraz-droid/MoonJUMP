using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Find : MonoBehaviour {
	GameObject Player;
	public bool jumping;
	// Use this for initialization
	void Start () {
		Player = GameObject.FindGameObjectWithTag ("Player");
	}
	void OnCollisionEnter(Collision col)
	{
		Debug.Log (col.gameObject.tag);
		if (col.gameObject == Player && !jumping) 
		{
			col.gameObject.GetComponent<Rigidbody> ().isKinematic = true;
			col.gameObject.GetComponent<Rigidbody> ().useGravity = false;

		} else 
		{
			col.gameObject.GetComponent<Rigidbody> ().isKinematic = false;
			col.gameObject.GetComponent<Rigidbody> ().useGravity = true;
		}
		if (jumping) {
			Player.GetComponent<Rigidbody> ().isKinematic = false;
			Player.GetComponent<Rigidbody> ().useGravity = true;
		}
	}
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) 
		{
			jumping = true;
		}
	}
}
