using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseDatabase : MonoBehaviour {

	public static HouseDatabase instance;

	// database
	private Dictionary<string, House> db = new Dictionary<string, House>();

	string[] allHouses = new[] { 
		// hey! if you change this, change each house's 
		// HouseController.houseID in unity as well!
		"MP", 
		"DD", 
		"ZZ"
	};

	public House getHouseByID(string houseID) {
		return db [houseID];
	}

	public bool isValidHouse(string houseID) {
		return db.ContainsKey (houseID); 
	}

//	public IEnumerable<string> getAllHouseIDs() {
//		return db.Keys;
//	}

	void Awake() {
		instance = this;
	}

	// Use this for initialization
	void Start () {
		for (int i = 0; i < allHouses.Length; i++) {
			string houseID = allHouses [i];
			House house = new House ();
			house.initialize(houseID);
			db[houseID] = house;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
