using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseController : MonoBehaviour {

	public string houseID;
	private House house;

	// Use this for initialization
	void Start () {
		house = HouseDatabase.instance.getHouseByID (houseID);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
