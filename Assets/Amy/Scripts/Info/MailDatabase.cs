using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailDatabase : MonoBehaviour {
	
	// database
	private Dictionary<string, Mail> db = new Dictionary<string, Mail>();

	// mailID | fromHouseID | toHouseID \ daysToRead | text
	string[] allMail = new[] {
		"1000|MP|DD|2|secret admirer letter",
		"1001|DD|QZ|1|Dear Secret Admirer,\n\nThe poems are nice, but tell me more about you.",
		"1002|MP|DD|1|response 2/part 2",
		"1003|DD|MP|3|response 3",
		"1004|MP|DD|1|response 4",
		"2001|MP|ZZ|1|other house blah"
	};

	private HashSet<int> allStories = new HashSet<int> ();
		
	public Mail getMailByID(string mailID) {
		return db [mailID];
	}

	public bool isValidMail(string mailID) {
		return db.ContainsKey (mailID);
	}

	public HashSet<int> getAllStories() {
		return allStories;
	}
		
	// Use this for initialization
	void Awake () {
		for (int i = 0; i < allMail.Length; i++) {
			string[] entries = allMail[i].Split ('|');

			// .mailID and story
			string mailID = entries[0];
			allStories.Add (Int32.Parse (entries [0]) / 1000);

			// .daysToRead
			int daysToRead = Int32.Parse (entries [3]);

			// .text
			string[] textArr = entries [4].Split('/');
			List<string> text = new List<string>();
			for (int m = 0; m < textArr.Length; m++) {
				text.Add(textArr[m]);
			}

			// actually populate
			Mail mail = new Mail ();
			mail.initialize(mailID, entries [1], entries [2], daysToRead, text);
			db [mailID] = mail;
		}
	}
}
