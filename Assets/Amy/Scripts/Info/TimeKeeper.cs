using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeKeeper : MonoBehaviour {
	// everyone else has mail and house IDs as strings
	// only TimeKeeper keeps track of mailID as int

	const bool TIMEKEEPER_DEBUG = true;

	[SerializeField]
	MailDatabase mailDb;
	[SerializeField]
	HouseDatabase houseDb;

	private int currDay = 0;
	private Dictionary<int, int> latestMailWaiting = new Dictionary<int, int>();
	private Dictionary<int, int> daysBeforeDeliver = new Dictionary<int, int>(); 
	// -1 means the latestMailWaiting is still waiting, no change
	// >= 1 means decrement daysBeforeDeliver
	// 0 means increment latestMailWaiting, make daysBeforeDeliver -1, put mail in house
	// when mail is delivered, set daysToDeliver to proper number (mail.daysToRead)

	// helper functions for conversion
	private int getMailIDInt(string mailIDString) {
		return Int32.Parse (mailIDString);
	}

	private int getStoryIDInt(string mailIDString) {
		return getMailIDInt (mailIDString) / 1000;
	}

	private int getMailNumInt(string mailIDString) {
		return getMailIDInt (mailIDString) % 1000;
	}

	private string getMailIDString(int storyIDInt, int mailNumInt) {
		int mailIDInt = storyIDInt * 1000 + mailNumInt;
		return mailIDInt.ToString ();
	}


	// helper function for incrementDay
	private void initMail(int storyID) {
		// update daysBeforeDeliver
		daysBeforeDeliver[storyID] = -1;

		// update latestMailWaiting
		latestMailWaiting[storyID]++;

		// actually init the mail
		string currMailID = getMailIDString(storyID, latestMailWaiting[storyID]);
		Mail currMail = mailDb.getMailByID (currMailID);
		House currHouse = houseDb.getHouseByID (currMail.getFromHouseID ());
		currHouse.addMail (currMailID);
	}

	void incrementDay() {
		// update currDay
		currDay++;

		foreach (int storyID in mailDb.getAllStories ()) {
			// update daysBeforeDeliver
			if (daysBeforeDeliver[storyID] != -1) {
				daysBeforeDeliver [storyID]--;

				// update latestMailWaiting
				if (daysBeforeDeliver [storyID] == 0) {
					initMail (storyID);
				}
			}
		}

		if (TIMEKEEPER_DEBUG) {
			Debug.Log ("TimeKeeper: day was incremented to " + currDay);
			foreach (int storyID in mailDb.getAllStories ()) {
				Debug.Log("    storyID " + storyID + " has latestMailWaiting + " + 
					latestMailWaiting[storyID] + " and daysBeforeDeliver " + daysBeforeDeliver[storyID]);
			}
		}
	}

	public void logMailDelivered(string mailID) {
		Mail currMail = mailDb.getMailByID (mailID);
		int daysToRead = currMail.getDaysToRead ();
		int storyID = getStoryIDInt (mailID);
		daysBeforeDeliver [storyID] = daysToRead;

		if (TIMEKEEPER_DEBUG) {
			Debug.Log ("TimeKeeper: mail with ID " + mailID + " was logged as delivered");
		}
	}

	// Use this for initialization
	void Start () {
		foreach (int storyID in mailDb.getAllStories ()) {
			latestMailWaiting [storyID] = 0;
			daysBeforeDeliver [storyID] = 0; 
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
