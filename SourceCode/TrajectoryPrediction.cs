using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryPrediction : MonoBehaviour {
	bool isDead = false;
	bool Won = false;
	[Header("Line Renderer Variables")]
	public LineRenderer line;
	public int resolution;

	[Header("Formula Variables")]
	public float y_velocity, x_velocity;
	public float ylimit;
	public float g;
	public float multiplier_x,multiplier_y;

	[Header("Camera : ")]
	public Camera cam;
	public int cam_multiplier;

	[Header("RigidBody : ")]
	Rigidbody rb;
	[Header("Jump : ")]
	public float jumpspeed;
	public bool jumpbool;

	[Header("Particle Systems : ")]
	public ParticleSystem Puff;
	public ParticleSystem explosion;
	public ParticleSystem Earth_Puff;
	public ParticleSystem Pickup;
	public ParticleSystem explosion_sun;

	[Header("Mesh : ")]
	public GameObject mesh;

	[Header("Canvas : ")]
	public GameObject ded;
	public GameObject Pause_object;
	public bool paused;
	public GameObject Won_canvas;

	[Header("Scope : ")]
	public	GameObject Scope_2x;
	public GameObject Scope_4x;

	[Header("Sound_Effect :")]
	public AudioSource Jump_Sound;
	public AudioSource Scope, Death, Complete; 

	[Header("ClickRate : ")]
	private float click_rate = 0f;
	public float clickfire= 2f;
	IEnumerator RenderArc()
	{
		
		line.positionCount = resolution + 1;
		line.SetPositions(CalculateLineArray());
		yield return null;
	}
	Vector3[] CalculateLineArray()
	{
		Vector3[] LineArray = new Vector3[resolution + 1];
		for (int i = 0; i < LineArray.Length; i++) {
			var t = i / (float)LineArray.Length;
			LineArray[i] = CalculateLinePoint (t);
		}
		return LineArray;
	}
	Vector3 CalculateLinePoint (float t)
	{
		
		float x = x_velocity * t;
		float y = (y_velocity * t) - g*(Mathf.Pow (t, 2))*1/2;
		return new Vector3 (x , y , 0);
	}

	// Use this for initialization
	void Start () {
		g = Mathf.Abs (Physics.gravity.y);
		rb = this.GetComponent<Rigidbody> ();
		cam_multiplier = 1;
		y_velocity = Mathf.Clamp (y_velocity, -20f, 20f);
		x_velocity = Mathf.Clamp (x_velocity,-25f, 25f);
	}
	
	// Update is called once per frame
	void Update () {
		Pause ();

		cam.GetComponent<Camera_Follow> ().multiplier = cam_multiplier;
		if (!isDead && !paused && !Won && jumpbool) {
			line.gameObject.SetActive (true);
			Mouse_Pos ();
			Jump ();
			StartCoroutine (RenderArc ());
		} else
			line.gameObject.SetActive (false);
	}
	void Mouse_Pos()
	{
		
		Vector3 mousePositon = Input.mousePosition;
		Vector3 worldpos = cam.ScreenToWorldPoint (new Vector3(mousePositon.x,mousePositon.y,-5.89f));
		//Debug.Log (worldpos);
		y_velocity = this.transform.position.y - worldpos.y;
		x_velocity = this.transform.position.x - worldpos.x;
		y_velocity *= multiplier_y;
		x_velocity *= multiplier_x;
		Debug.Log (y_velocity + " " + x_velocity);
		y_velocity = Mathf.Clamp (y_velocity, -20f, 20f);
		x_velocity = Mathf.Clamp (x_velocity,-25f, 25f);
	}
	void Jump()
	{
		if(Input.GetKeyDown(KeyCode.Mouse0) && jumpbool && Time.time >= click_rate)
		{
			click_rate = Time.time + 1 / clickfire;
			Vector3 jump_vel = new Vector3(x_velocity,y_velocity,0);
		//	Debug.Log (jump_vel);
			Jump_Sound.Play ();
			rb.velocity = jump_vel*jumpspeed;
			//transform.rotation = Quaternion.LookRotation (new Vector3 (0,0,rb.velocity.z));
		}
	}
	void OnCollisionEnter(Collision col)
	{
		//Debug.Log (col.gameObject.tag);
		if (col.gameObject.tag == "Planet") {
			rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationY;
			rb.velocity = Vector3.zero;
			Puff.transform.position = col.contacts [0].point;
			Puff.Play ();
			jumpbool = true;

		} else if (col.gameObject.tag == "Earth") 
		{
		} else {
			rb.constraints = RigidbodyConstraints.None;
		}
		if (col.gameObject.name == "Death") {
			Death.Play ();
			mesh.GetComponent<MeshRenderer> ().enabled = false;
			line.gameObject.SetActive (false);
			explosion.Play ();
			ded.SetActive (true);
			isDead = true;
		}
		if (col.gameObject.tag == "Death") {
			Death.Play ();
			mesh.GetComponent<MeshRenderer> ().enabled = false;
			line.gameObject.SetActive (false);
			explosion_sun.transform.position = col.contacts [0].point;
			rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationY;
			explosion_sun.Play ();
			ded.SetActive (true);
			isDead = true;
		}

	}
	void Pause()
	{
		if (Input.GetKeyDown (KeyCode.P)) 
		{
			if (paused) {			
				StartCoroutine (Pause_False ());
			} else {
				Pause_object.SetActive (true);
				paused = true;
			}
		}
	}
	void OnCollisionExit(Collision col)
	{
		if (col.gameObject.tag == "Planet") {
			//Debug.Log ("not freeze");
			jumpbool = false;
			rb.constraints = RigidbodyConstraints.None;
			rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ;

		}
	}
	void OnTriggerEnter(Collider col)
	{
		Debug.Log (col.gameObject.tag);
		if (col.gameObject.tag == "Earth") {
			rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationY;
			Earth_Puff.transform.position = col.ClosestPoint(this.gameObject.transform.position);
			Earth_Puff.Play ();
			Won_canvas.SetActive (true);
			Won = true;
			Complete.Play ();
		}
		if (col.gameObject.tag == "2x") {
			cam_multiplier = 2;
			Scope_2x.SetActive (true);
			Pickup.transform.position = col.ClosestPoint (this.transform.position);
			Pickup.Play ();
			Scope.Play ();
			Destroy (col.gameObject);
		}
		if (col.gameObject.tag == "4x") 
		{
			cam_multiplier = 4;
			Scope_4x.SetActive (true);
			Pickup.transform.position = col.ClosestPoint (this.transform.position);
			Pickup.Play ();
			Scope.Play ();
			Destroy (col.gameObject);
		}

	}
	IEnumerator Pause_False()
	{
		Pause_object.GetComponent<Animator> ().SetBool ("End", true);
		yield return new WaitForSeconds (1f);
		Pause_object.GetComponent<Animator> ().SetBool ("End", false);
		Pause_object.SetActive (false);
		paused = false;
	}
	public void Camera_Multiplier_Change(int t)
	{
		cam_multiplier = t;
	}

}
