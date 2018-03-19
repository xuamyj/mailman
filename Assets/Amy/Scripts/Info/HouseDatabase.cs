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
		"FamilyDad|1 Moon St|Cameron", 
		"2W|2 Windy Dr|2W_name", 
		"3W|3 Windy Dr|3W_name",
		"Lonely|4 Windy Dr|Iman",
		"1H|1 Hyde Way|1H_name",
		"KidMove|2 Hyde Way|Saranya",
		"3H|3 Hyde Way|3H_name",
		"4H|4 Hyde Way|4H_name",
		"KidStay|5 Hyde Way|Addie",
		"1P|1 Pond Ave|1P_name",
		"CrushSecret|2 Pond Ave|Toni",
		"3P|3 Pond Ave|3P_name",
		"KidNew|4 Pond Ave|4P_name",//"KidNew|4 Pond Ave|Saranya"
		"1L|1 Leaf St|1L_name",
		"2L|2 Leaf St|2L_name",
		"CrushReceive|3 Leaf St|Jeff",
		"FamilyGirl|4 Leaf St|Cassie",
		"5L|5 Leaf St|5L_name",
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
