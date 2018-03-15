using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MailController : MonoBehaviour {

	[SerializeField]
	Transform cubeTransform;

	[SerializeField]
	TextMeshPro letterTextDisplay;

	Mail mail;

	void Init(Mail mail){
		this.mail = mail;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
