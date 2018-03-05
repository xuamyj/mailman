using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House {

	const bool HOUSE_DEBUG = true;

	// stays the same
	private string houseID;

	// changes
	private List<string> mailToSend = new List<string>();

	public void initialize(string houseIDVal) {
		houseID = houseIDVal;
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
			mailToSend.Clear();

			if (HOUSE_DEBUG) {
				Debug.Log ("House: mail was picked up from house " + houseID);
			}
			return result;
		} else {
			return null;
		}
	}

	// getters
	string getHouseID() {
		return houseID;
	}

	public bool checkHasMailToSend() {
		return mailToSend.Count != 0;
	}

}
