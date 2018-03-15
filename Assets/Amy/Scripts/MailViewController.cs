using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class MailViewController : MonoBehaviour {
	[SerializeField]
	TextMeshPro fromText;
	[SerializeField]
	TextMeshPro toText;
	[SerializeField]
	TextMeshPro bodyText;
	[SerializeField]
	GameObject movableObject;

	bool flipped = false;

	public void setVisible(bool visibility)
	{
		if (visibility == false) {
			fromText.text = "";
			toText.text = "";
			bodyText.text = "";
			movableObject.SetActive (false);
		} else {
			movableObject.SetActive (true);
		}
	}

	public void rotate(){
		Debug.Log ("doing rotate");
		flipped = !flipped;
		movableObject.transform.DOLocalRotate (new Vector3(0.0f, flipped ? 180.0f : 0.0f), 0.5f, RotateMode.FastBeyond360);
	}

	public void render(Mail mail){
		movableObject.transform.localRotation = Quaternion.identity;
		House fromHouse = HouseDatabase.instance.getHouseByID (mail.getFromHouseID());
		House toHouse = HouseDatabase.instance.getHouseByID (mail.getToHouseID());
		fromText.text = string.Format ("FROM: {0}\n{1}",
			fromHouse.getResidentName (), fromHouse.getHouseAddress ());
		toText.text = string.Format ("TO: {0}\n{1}",
			toHouse.getResidentName (), toHouse.getHouseAddress ());
		bodyText.text = mail.getText () [0];
	}
}
