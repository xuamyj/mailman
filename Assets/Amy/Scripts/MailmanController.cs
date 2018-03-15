using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailmanController : MonoBehaviour {

	private HouseController currHouseTouching = null;
	private Dashboard dashboard;

	private Rigidbody rb;

	const float VELOCITY_CAP_FACTOR = 0.9f;
	const float FORCE_MULT_FACTOR = 200.0f;
	const float TURN_EASINESS_FACTOR = 8.0f;

	// Use this for initialization
	void Start () {
		dashboard = GetComponent<Dashboard> ();
		rb = GetComponent<Rigidbody> ();
	}

	// https://answers.unity.com/questions/35541/problem-finding-relative-rotation-from-one-quatern.html
	//	THIS DID NOT WORK! but good try... 
	//	Quaternion getRelativeRotation(Quaternion a, Quaternion b) {
	//		return Quaternion.Inverse (a) * b;
	//	}

	void FixedUpdate() {
		Vector3 force = new Vector3 (Input.GetAxis("Horizontal"), 
			0.0f, Input.GetAxis("Vertical"));
		if (force.magnitude > 0.1f) {
			// add force
			rb.AddForce (force * FORCE_MULT_FACTOR);

			// add "air resistance" (caps speed)
			rb.velocity *= VELOCITY_CAP_FACTOR;

			// don't roll
			float yRot = this.transform.eulerAngles.y;
			this.transform.eulerAngles = new Vector3(0.0f, yRot, 0.0f);

			// apply torque toward proper direction
			// Vector3 lookPos = new Vector3(rb.velocity.x, 0.0f, rb.velocity.z); face velocity
			Vector3 lookPos = force;
			var rotation = Quaternion.LookRotation (lookPos);
			transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * TURN_EASINESS_FACTOR);
			// THIS DID NOT WORK! but good try... rb.AddTorque(rotationToAdd.eulerAngles * 0.1f); 
		} else {
			// TODO: need to add "screech" animation here
			rb.velocity = Vector3.zero;
		}
	}

	void Update()
	{
		// pick up mail
		if (Input.GetKeyDown (KeyCode.RightShift)) {
			if (currHouseTouching) {
				dashboard.pickupMail (currHouseTouching.houseID);
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
