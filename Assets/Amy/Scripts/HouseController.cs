using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseController : MonoBehaviour {

	public string houseID;
	private House house;
	[SerializeField]
	GameObject exclamationObject;

	private bool hasMail = true;

	// Use this for initialization
	void Start () {
		house = HouseDatabase.instance.getHouseByID (houseID);
	}
	
	// Update is called once per frame
	void Update () {
		if (house != null && hasMail != house.checkHasMailToSend () && exclamationObject != null) {
			hasMail = house.checkHasMailToSend ();
			exclamationObject.SetActive (hasMail);
		}
	}
}
