using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MailController : MonoBehaviour {

	[SerializeField]
	Image background;

	[SerializeField]
	TextMeshProUGUI letterTextDisplay;

	[SerializeField]
	Color activeColor;

	[SerializeField]
	Color inactiveColor;

	Mail mail;
	RectTransform rectTransform;

	bool isSelected;

	void Awake(){
		rectTransform = GetComponent<RectTransform> ();
		background.color = inactiveColor;
	}

	public Mail getMail(){
		return mail;
	}

	public void setSelected(bool isSelected){
		this.isSelected = isSelected;
		background.color = isSelected ? activeColor : inactiveColor; 
	}

	public void Init(Mail mail){
		this.mail = mail;
		House toHouse = HouseDatabase.instance.getHouseByID (mail.getToHouseID());
		House fromHouse = HouseDatabase.instance.getHouseByID (mail.getFromHouseID());
		letterTextDisplay.text = "TO: " + toHouse.getHouseAddress ();
		rectTransform.anchoredPosition3D = Vector3.zero;
		rectTransform.localRotation = Quaternion.identity;
	}
}
