using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision_Player : MonoBehaviour {

	public GameObject Player;
	void OnCollisionEnter(Collision col)
	{
		Debug.Log (col.gameObject.tag);
		if (col.gameObject.tag == "Planet") {
			Player.GetComponent<Rigidbody> ().useGravity = false;
		} else
			Player.GetComponent<Rigidbody> ().useGravity = true;
	}
}
