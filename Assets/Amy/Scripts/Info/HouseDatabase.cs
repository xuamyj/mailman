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
		"MP|1 Rose|Leaf Club Leader", 
		"DD|2 Oak|Jamie", 
		"ZZ|1 Lake|Sleepy Cat",
		"QZ|???|Secret Admirer",
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
		for (int i = 0; i < allHouses.Length; i++) {
			string[] entries = allHouses[i].Split ('|');
			string houseID = entries[0];

			House house = new House ();
			house.initialize(houseID, entries [1], entries [2]);
			db[houseID] = house;
		}
	}
}
