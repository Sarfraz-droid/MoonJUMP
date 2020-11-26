using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour {
	public Transform target;
	public float smoothSpeed = 0.125f;
	public Vector3 offset;
	public int multiplier;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		Vector3 desiredPosition = target.position + new Vector3(0,0,offset.z*multiplier);
		Vector3 SmoothPosition = Vector3.Lerp (transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
		transform.position = SmoothPosition;
	}
}
