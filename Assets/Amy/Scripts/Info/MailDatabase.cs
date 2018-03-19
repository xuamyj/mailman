using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailDatabase : MonoBehaviour {
	
	// database
	private Dictionary<string, Mail> db = new Dictionary<string, Mail>();

	// mailID | fromHouseID | toHouseID \ daysToRead | startDay | text
	//string[] allMail = new[] {
	//	"1001|KidMove|KidStay|1|1|Dear Addie,\n\nI'm so, so, SO sorry I broke your Tamagotchi. I'm the worst Best Best Friend ever!",
	//	"5001|1H|3H|10|1|Hey, thanks for letting me borrow your lawnmower.",
	//	"6001|4H|1H|10|1|I found a great log in the woods.\n\nWhen you get a chance, you should come see it.",
	//	"1002|KidStay|KidMove|1|X|Dear Saranya,\n\nAw, it's OK! Just let me play with yours whenever I want.",
	//	"2001|KidMove|CrushSecret|2|2|Dear Toni,\n\nHave a mice birthday! Mouse picture below. You're the best babysitter ever!"
	//};
	string[] allMail = new[] {
		"1001|KidMove|KidStay|1|Dear Addie,\n\nI'm so, so, SO sorry I broke your Tamagotchi. I'm the worst Best Best Friend ever!",
		"5001|1H|3H|10|Hey, thanks for letting me borrow your lawnmower.",
		"6001|4H|1H|10|I found a great log in the woods.\n\nWhen you get a chance, you should come see it.",
		"1002|KidStay|KidMove|1|Dear Saranya,\n\nAw, it's OK! Just let me play with yours whenever I want.",
		"2001|KidMove|CrushSecret|2|Dear Toni,\n\nHave a mice birthday! Mouse picture below. You're the best babysitter ever!"
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

			// .startDay
			// uncomment and update Mail, TimeKeeper, etc. when not so sleepy
			//int startDay = Int32.Parse (entries [4]);

			// .text
			string[] textArr = entries [4].Split('/');
			//string[] textArr = entries [5].Split('/');
			List<string> text = new List<string>();
			for (int m = 0; m < textArr.Length; m++) {
				text.Add(textArr[m]);
			}

			// actually populate
			Mail mail = new Mail ();
						mail.initialize(mailID, entries [1], entries [2], daysToRead, text);
			//mail.initialize(mailID, entries [1], entries [2], daysToRead, startDay, text);
			db [mailID] = mail;
		}
	}
}
