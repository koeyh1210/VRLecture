﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	//mouse controll var
	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }

	public RotationAxes axes = RotationAxes.MouseXAndY;

	public float movementSpeed = 10.0f;
	public float sensitivityX = 15F;
	public float sensitivityY = 15F;
	public float minimumX = -360F;
	public float maximumX = 360F;
	public float minimumY = -90F;
	public float maximumY = 90F;
	float rotationY = 0F;



	private Vector3 mouseVector;

	private Rigidbody rb;


	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		if (rb) {
			rb.freezeRotation = true;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		movePlayer ();
		rotatePlayer ();
	}

	void OnCollisionEnter(Collision collision){
		
		if (collision.gameObject.tag == "Map") {
			rb.velocity = Vector3.zero;
		}
	}

	void OnCollisionExit(Collision collision){
		if (collision.gameObject.tag == "Map") {
			rb.velocity = Vector3.zero;
		}
	}

	void movePlayer(){
		if(Input.GetKey(KeyCode.W)) {
			rb.position += rb.transform.forward * Time.deltaTime * movementSpeed;
		}
		else if(Input.GetKey(KeyCode.S)) {
			rb.position += -rb.transform.forward * Time.deltaTime * movementSpeed;
		}
		if(Input.GetKey(KeyCode.A)) {
			rb.position += -rb.transform.right * Time.deltaTime * movementSpeed;
		}
		else if(Input.GetKey(KeyCode.D)) {
			rb.position += rb.transform.right * Time.deltaTime * movementSpeed;
		}
	}

	//rotate player with mouse
	void rotatePlayer(){
		if (axes == RotationAxes.MouseXAndY)
		{
			float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;

			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);

			transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
		}
	}
}
