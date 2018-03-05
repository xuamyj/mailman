using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailmanController : MonoBehaviour {

	private HouseController currHouseTouching = null;
	private Dashboard dashboard;

	// Use this for initialization
	void Start () {
		dashboard = GetComponent<Dashboard> ();
	}
	
	void Update()
	{
		var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
		var z = Input.GetAxis("Vertical") * Time.deltaTime * 12.0f;

		transform.Rotate(0, x, 0);
		transform.Translate(0, 0, z);

		if (Input.GetKeyDown ("space")) {
			if (currHouseTouching) {
				dashboard.pickupMail (currHouseTouching.houseID);
			}
		}

//		onV() {
//			// get the house we are currently 'touching'
//			// get mail
//		}
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
