using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transform_copy : MonoBehaviour {
	GameObject Player;
	public Vector3 offset;
	// Use this for initialization
	void Start () {
		Player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		transform.rotation = Quaternion.Euler (0, 0, Player.transform.rotation.z * -1);

	}
}
