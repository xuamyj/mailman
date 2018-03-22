using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class AlertController : MonoBehaviour {

	public static AlertController instance;

	[SerializeField]
	private GameObject imageGameObject;

	[SerializeField]
	private GameObject textMeshObject;

	private TextMeshProUGUI textMesh;
	private Coroutine coroutine;

	private List<string> displayStrings = new List<string>();

	void Awake () {
		instance = this;
		textMesh = textMeshObject.GetComponent<TextMeshProUGUI> ();
		imageGameObject.SetActive (false);
	}

	IEnumerator hideMessage(string message){
		yield return new WaitForSeconds(1.5f);
		Debug.Log (message);
		displayStrings.Remove (message);
		if (displayStrings.Count > 0) {
			showMessages();
		} else {
			imageGameObject.SetActive (false);
		}
	}

	public void showAlert(string message) {
		if (message == "")
			return;
		imageGameObject.SetActive (true);
		displayStrings.Add (message);
		showMessages ();

		StartCoroutine (hideMessage(message));
	}

	private void showMessages(){
		textMesh.text = string.Join ("\n", displayStrings.ToArray ());
	}

}
