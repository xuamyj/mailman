using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Dashboard : MonoBehaviour {

	const bool DASHBOARD_DEBUG = true;

	// stays the same
	[SerializeField]
	MailDatabase mailDb;
	[SerializeField]
	HouseDatabase houseDb;
	[SerializeField]
	TimeKeeper timeKeeper;

	// changes
	private List<string> mailBeingHeld = new List<string>();
	private float milesToday = 0.0f;
	private int deliveryCount = 0;

	public event Action<List<string>> mailHeldChanged;
		
	void drive (float milesDriven = 0.1f) {
		milesToday += milesDriven;

		if (DASHBOARD_DEBUG) {
			Debug.Log ("Dashboard: so far, we have driven " +  milesToday + " miles today.");
		}
	}

	public void deliverMailToHouse(string houseId)
	{
		List<string> mails = new List<string> (mailBeingHeld);
		foreach (string mailID in mails) {
			Debug.Log(deliverMail (mailID, houseId));
		}
	}


	// returns house response
	// TODO: add interesting responses per house
	string deliverMail (string mailID, string houseID) {
		// check for valid mailID and houseID
		if (!mailDb.isValidMail(mailID) || !mailBeingHeld.Contains(mailID)) {
			throw new System.Exception("Dashboard: deliverMail() - invalid mailID");
		}
		Mail mail = mailDb.getMailByID(mailID);
		if (!houseDb.isValidHouse (houseID)) {
			throw new System.Exception ("Dashboard: deliverMail() - invalid houseID");
		}

		// check mail belongs to this house
		if (mail.getToHouseID() != houseID) {
			if (DASHBOARD_DEBUG) {
				Debug.Log ("Dashboard: mail with ID " + mailID + " does not belong to house " + houseID);
			}
			return "Sorry, I don't think that mail is for me!";
		
		// real code here
		} else {
			mail.setDelivered ();
			mailBeingHeld.Remove (mailID);
			deliveryCount++;
			timeKeeper.logMailDelivered (mailID);

			if (DASHBOARD_DEBUG) {
				Debug.Log ("Dashboard: delivered mail with ID " + mailID + " to house " + houseID);
			}

			if (mailHeldChanged != null){
				mailHeldChanged (mailBeingHeld);
			}
			return "Thanks!";
		}
	}

	// returns house response
	// TODO: add interesting responses per house
	public string pickupMail(string houseID) {
		// check for valid houseID
		if (!houseDb.isValidHouse (houseID)) {
			throw new System.Exception ("Dashboard: pickupMail() _ invalid houseID");
		}
		House house = houseDb.getHouseByID(houseID);

		// check house has mail
		if (!house.checkHasMailToSend()) {
			if (DASHBOARD_DEBUG) {
				Debug.Log ("Dashboard: house " + houseID + " does not have mail to deliver");
			}
			return "Sorry, I don't have any mail for you!";
		} else {
			List<string> houseMail = house.pickupMailToSend ();
			for (int i = 0; i < houseMail.Count; i++) {
				string mailID = houseMail [i];
				Mail mail = mailDb.getMailByID (mailID);
				mail.setPicked ();
				mailBeingHeld.Add (mailID);
				if (DASHBOARD_DEBUG) {
					Debug.Log ("Dashboard: picked up mail with ID " + mailID + " from house " + houseID);
				}
			}

			if (mailHeldChanged != null){
				mailHeldChanged (mailBeingHeld);
			}
			return "Yay, it's the mailman!";
		}
	}

	// getters
	List<Mail> getDashboardMail() {
		List<Mail> dashboardMail = new List<Mail> ();
		for (int i = 0; i < mailBeingHeld.Count; i++) {
			dashboardMail.Add(mailDb.getMailByID(mailBeingHeld[i]));
		}
		return dashboardMail;
	}

	double getMilesToday() {
		return milesToday;
	}

	int getDeliveryCount() {
		return deliveryCount;
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
