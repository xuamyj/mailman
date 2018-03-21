using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MailmanController : MonoBehaviour {

	private HouseController currHouseTouching = null;
	private Dashboard dashboard;

	private Rigidbody rb;
	Camera mainCamera;

	const float VELOCITY_CAP_FACTOR = 0.9f;
	[SerializeField]
	float FORCE_MULT_FACTOR = 20000.0f;
	[SerializeField]
	float DASH_MULT_FACTOR = 20000.0f;
	[SerializeField]
	float TORQUE_MULT_FACTOR = 100.0f;
	const float TURN_EASINESS_FACTOR = 8.0f;


	[SerializeField]
	float UP_ROT_ANGLE = 15.0f;
	[SerializeField]
	float RIGHT_ROT_ANGLE = 10.0f;
	[SerializeField]
	float UP_POS_MOVE = 16.6f;
	[SerializeField]
	float BACK_POS_MOVE = -51.2f;
	[SerializeField]
	float FRONT_LOOK_AT_MOVE = 10.0f;

	[SerializeField]
	float CAMERA_TWEEN_CONST = 0.2f;

	[SerializeField]
	float JUMP_FACTOR = 120.0f;

	// Use this for initialization
	void Start () {
		dashboard = GetComponent<Dashboard> ();
		rb = GetComponent<Rigidbody> ();
		// rb.maxAngularVelocity = 1.0f;

		mainCamera = Camera.main;
	}

	// https://answers.unity.com/questions/35541/problem-finding-relative-rotation-from-one-quatern.html
	//	THIS DID NOT WORK! but good try... 
	//	Quaternion getRelativeRotation(Quaternion a, Quaternion b) {
	//		return Quaternion.Inverse (a) * b;
	//	}
	
	void FixedUpdate() {
		if (CanvasController.instance.isOn ()) {
			return;
		}

		Vector3 forward = this.transform.forward;
		Vector3 up = Vector3.up;

		RaycastHit raycastHit;
		if (Physics.Raycast (this.transform.position, -Vector3.up, out raycastHit)) {
			forward = Vector3.ProjectOnPlane (forward, raycastHit.normal);
			up = raycastHit.normal;
		}

		Vector3 force = forward * Input.GetAxis ("Vertical") * FORCE_MULT_FACTOR * Time.fixedDeltaTime; 
		Vector3 torque = Mathf.Sign(Input.GetAxis ("Vertical"))* up * Input.GetAxis ("Horizontal") * TORQUE_MULT_FACTOR * Time.fixedDeltaTime;
		if (force.magnitude > 0.01f || torque.magnitude > 0.01f) {
			// add force
			rb.AddForce (force);
			rb.AddTorque (torque, ForceMode.Impulse);

			// add "air resistance" (caps speed)
			rb.velocity *= VELOCITY_CAP_FACTOR;


			// apply torque toward proper direction
			// THIS DID NOT WORK! but good try... rb.AddTorque(rotationToAdd.eulerAngles * 0.1f); 

			// for old version where we face the way we're holding
			// Vector3 lookPos = force;
			// var rotation = Quaternion.LookRotation (lookPos);
			// transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * TURN_EASINESS_FACTOR);
		}

		// jump
		if (Input.GetKeyDown (KeyCode.Space)) {
			rb.velocity = new Vector3 (0.0f, JUMP_FACTOR, 0.0f);
			// don't roll
			float yRot = this.transform.eulerAngles.y;
			this.transform.eulerAngles = new Vector3(0.0f, yRot, 0.0f);
		}

		// dash
		if (Input.GetKeyDown (KeyCode.RightCommand) || Input.GetKeyDown (KeyCode.LeftCommand)) {
			Vector3 dashForce = this.transform.forward * DASH_MULT_FACTOR;
			rb.AddForce (dashForce, ForceMode.Impulse);
		}
	}

	Vector3 rotateByAngle(Vector3 forwardVector, Vector3 axis, float angle) {
		return Quaternion.AngleAxis (angle, axis) * forwardVector;
	}


	int cameraUpdateCount = -1;
	void updateCamera() {
		cameraUpdateCount++;
		if (cameraUpdateCount % 10 != 0) {
			return;
		}
		// find truck vectors
		Vector3 truckForward = this.transform.forward;
		Vector3 truckRight = this.transform.right;
		Vector3 truckUp = this.transform.up;

		// find camera angle
		Vector3 cameraForward = rotateByAngle (truckForward, truckUp, UP_ROT_ANGLE); 
		cameraForward = rotateByAngle(cameraForward, truckRight, RIGHT_ROT_ANGLE);

		// find camera position
		Vector3 cameraPosition = this.transform.position + BACK_POS_MOVE * cameraForward;
		if (this.transform.position.y < UP_POS_MOVE / 3.0f) {
			cameraPosition.y = UP_POS_MOVE;
		} else {
			cameraPosition.y = UP_POS_MOVE + this.transform.position.y;
		}

		mainCamera.transform.DOMove(cameraPosition, CAMERA_TWEEN_CONST);
		mainCamera.transform.DOLookAt (this.transform.position + truckForward * FRONT_LOOK_AT_MOVE, CAMERA_TWEEN_CONST*2);
	}

	void Update()
	{
		updateCamera ();

		// pick up mail
		if (Input.GetKeyDown (KeyCode.RightShift) || Input.GetKeyDown (KeyCode.LeftShift)) {
			if (currHouseTouching) {
				dashboard.pickupMail (currHouseTouching.houseID);
				dashboard.deliverMailToHouse (currHouseTouching.houseID);
			}
		}
	}

	void OnTriggerEnter(Collider other) {
		HouseController houseCtrl = other.gameObject.GetComponent<HouseController> ();
		if (houseCtrl) {
			currHouseTouching = houseCtrl;
		}
	}

	void OnTriggerExit(Collider other) {
		currHouseTouching = null;
	}
}
