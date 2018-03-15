using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House {

	const bool HOUSE_DEBUG = true;

	// stays the same
	private string houseID; // "DD"
	private string houseAddress; // "1 Rose"
	private string residentName; // "Jamie"

	// changes
	private List<string> mailToSend = new List<string>();

	public void initialize(string houseIDVal, string houseAddressVal, string residentNameVal) {
		houseID = houseIDVal;
		houseAddress = houseAddressVal;
		residentName = residentNameVal;
	}

	public void addMail(string mailID) {
		mailToSend.Add (mailID);

		if (HOUSE_DEBUG) {
			Debug.Log ("House: mail with ID " + mailID + " was added to house " + houseID);
		}
	}

	public List<string> pickupMailToSend() {
		if (checkHasMailToSend()) {
			List<string> result = mailToSend;
			mailToSend = new List<string>();

			if (HOUSE_DEBUG) {
				Debug.Log ("House: mail was picked up from house " + houseID);
			}
			return result;
		} else {
			return null;
		}
	}

	// getters
	public string getHouseID() {
		return houseID;
	}

	public string getHouseAddress() {
		return houseAddress;
	}

	public string getResidentName() {
		return residentName;
	}

	public bool checkHasMailToSend() {
		return mailToSend.Count != 0;
	}

}
