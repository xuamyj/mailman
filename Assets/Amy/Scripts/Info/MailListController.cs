using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZObjectPools;

public class MailListController : MonoBehaviour {
	[SerializeField]
	Dashboard dashboard;
	[SerializeField]
	MailDatabase mailDb;
	[SerializeField]
	HouseDatabase houseDb;

	[SerializeField]
	MailViewController mailViewController;


	EZObjectPool mailPool;

	List<MailController> usedMails = new List<MailController>();

	int cursorPos = 0;

	// Use this for initialization
	void Start () {
		mailPool = GetComponent<EZObjectPool> ();
		dashboard.mailHeldChanged += renderMail;	
	}
		
	void makeMail (string mailId) {
		GameObject mailObject;
		if (mailPool.TryGetNextObject(Vector3.zero, Quaternion.identity, out mailObject)) {
			MailController mailController = mailObject.GetComponent<MailController>();
			mailController.Init(mailDb.getMailByID(mailId));
			usedMails.Add (mailController);
		}
	}

	private void setCursorPos(int newPos){
		if (newPos < 0) {
			newPos += usedMails.Count;
		}
		usedMails [cursorPos].setSelected (false);
		usedMails [newPos].setSelected (true);
		cursorPos = newPos;

		mailViewController.render (usedMails [cursorPos].getMail());
	}

	public void isVisible()
	{
		cursorPos = 0;
		if (cursorPos == usedMails.Count) {
			Debug.Log ("No mails to show");
			mailViewController.setVisible (false);
		} else {
			mailViewController.setVisible (true);
			setCursorPos (0);
		}
	}

	public void onHorizontalInput(int dir){
		mailViewController.rotate ();
	}

	public void onVerticalInput(int dir){
		Debug.Log ("vertical input " + dir);
		setCursorPos ((cursorPos - dir) % usedMails.Count);
	}

	void renderMail(List<string> mailList){
		Debug.Log ("render mail");
		foreach (MailController mailController in usedMails) {
			mailController.gameObject.SetActive (false);
		}
		usedMails.Clear ();

		foreach (string mailId in mailList) {
			makeMail (mailId);
		}
	}
}
