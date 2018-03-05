﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailmanController : MonoBehaviour {

	[SerializeField]
	private CanvasController canvasCtrl;
	private GameObject canvasCtrlGameObj;
	private bool canvasOn = false;

	private HouseController currHouseTouching = null;
	private Dashboard dashboard;

	// Use this for initialization
	void Start () {
		dashboard = GetComponent<Dashboard> ();
		canvasCtrlGameObj = canvasCtrl.gameObject;
	}
	
	void Update()
	{
		var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
		var z = Input.GetAxis("Vertical") * Time.deltaTime * 12.0f;

		transform.Rotate(0, x, 0);
		transform.Translate(0, 0, z);

		if (Input.GetKeyDown (KeyCode.Space)) {
			if (currHouseTouching) {
				dashboard.pickupMail (currHouseTouching.houseID);
			}
		}

		if (Input.GetKeyDown (KeyCode.Return)) {
			if (canvasOn) {
				canvasCtrlGameObj.SetActive (false);
				canvasOn = false;
			} else {
				canvasCtrlGameObj.SetActive (true);
				canvasOn = true;
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
