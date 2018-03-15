using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CanvasController : MonoBehaviour {

	public static CanvasController instance;

	[SerializeField]
	MailListController mailListController;


	Camera mainCamera;
	bool canvasOn;
	float prevframeBottomRightOn = 0.0f;
	float bottomRightOn = 0.0f;

	const float DIST_FROM_CAMERA = 5.0f;
	[SerializeField]
	float CANVAS_SCALE = 0.05f;
	float SHRINK_SCALE = 10.0f;
	const float ANIMATION_SPEED = 0.5f;

	void Awake(){
		instance = this;
	}

	// Use this for initialization
	void Start () {
		mainCamera = Camera.main;
		canvasOn = false;
		bottomRightOn = 1.0f;
		this.transform.localScale = Vector3.one * DIST_FROM_CAMERA * CANVAS_SCALE / SHRINK_SCALE;
	}

	public bool isOn(){
		return canvasOn;
	}
	
	// Update is called once per frame
	void Update () {
		if (canvasOn) {
			if (Input.GetKeyDown (KeyCode.DownArrow) || Input.GetKeyDown (KeyCode.S)) {
				mailListController.onVerticalInput (-1);
			} else if (Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown (KeyCode.W)) {
				mailListController.onVerticalInput (1);
			} else if (Input.GetKeyDown (KeyCode.LeftArrow) || Input.GetKeyDown (KeyCode.A)) {
				mailListController.onHorizontalInput (-1);
			} else if (Input.GetKeyDown (KeyCode.RightArrow) || Input.GetKeyDown (KeyCode.D)) {
				mailListController.onHorizontalInput (1);
			}
		}
			
		if (Input.GetKeyDown (KeyCode.Return)) {
			if (canvasOn) {
				this.transform.DOScale (Vector3.one * DIST_FROM_CAMERA * CANVAS_SCALE / SHRINK_SCALE, ANIMATION_SPEED);
				DOTween.To (() => bottomRightOn, x => bottomRightOn = x, 1.0f, 
					ANIMATION_SPEED);
				canvasOn = false;
			} else { // canvasOn is false
				this.transform.DOScale (Vector3.one * DIST_FROM_CAMERA * CANVAS_SCALE, ANIMATION_SPEED);
				DOTween.To (() => bottomRightOn, x => bottomRightOn = x, 0.0f, 
					ANIMATION_SPEED);
				canvasOn = true;
				mailListController.isVisible ();
			}
		}

		if (prevframeBottomRightOn != bottomRightOn) {
			Debug.Log ("hi");
			Vector3 posOffset = mainCamera.transform.forward * DIST_FROM_CAMERA;
			// center, face us
			Vector3 newPosition = mainCamera.transform.position + posOffset;
			Quaternion newRotation = Quaternion.LookRotation (posOffset);
			this.transform.rotation = newRotation;
			// move to bottom right as necessary using bottomRightOn
			Vector3 bottomRightOffset = newRotation * Vector3.up * (-0.47f * DIST_FROM_CAMERA) + 
				newRotation * Vector3.right * (0.85f * DIST_FROM_CAMERA);
			this.transform.position = newPosition + bottomRightOffset * bottomRightOn;
		}

		prevframeBottomRightOn = bottomRightOn;
	}
}
