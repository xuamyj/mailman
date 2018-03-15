using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mail {

	enum MailState {None, Waiting, Picked, Delivered, Lost};
	const bool MAIL_DEBUG = true;

	// stays the same
	private string mailID;
	private string fromHouseID;
	private string toHouseID;
	private int daysToRead;
	private List<string> text = new List<string>();

	// changes
	private MailState status;

	public void initialize(string mailIDVal, string fromHouseIDVal, string toHouseIDVal, 
		int daysToReadVal, List<string> textVal) {
		mailID = mailIDVal;
		fromHouseID = fromHouseIDVal;
		toHouseID = toHouseIDVal;
		daysToRead = daysToReadVal;
		text = textVal;
		status = MailState.None;
	}

	void setWaiting() {
		status = MailState.Waiting;

		if (MAIL_DEBUG) {
			Debug.Log ("Mail: mail with ID " + mailID + " is waiting to be picked up");
		}
	}

	public void setPicked() {
		status = MailState.Picked;

		if (MAIL_DEBUG) {
			Debug.Log ("Mail: mail with ID " + mailID + " was picked up");
		}
	}

	public void setDelivered() {
		status = MailState.Delivered;

		if (MAIL_DEBUG) {
			Debug.Log ("Mail: mail with ID " + mailID + " was delivered");
		}
	}

	// getters 
	public string getMailID() {
		return mailID;
	}

	public string getFromHouseID() {
		return fromHouseID;
	}

	public string getToHouseID() {
		return toHouseID;
	}

	public int getDaysToRead() {
		return daysToRead;
	}

	public List<string> getText() {
		return text;
	}

	MailState getDelivered() {
		return status;
	}

}
