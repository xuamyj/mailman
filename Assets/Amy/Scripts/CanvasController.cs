using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CanvasController : MonoBehaviour {

	Camera mainCamera;
	bool canvasOn;
	float bottomRightOn;

	const float DIST_FROM_CAMERA = 18.0f;
	const float ANIMATION_SPEED = 0.75f;

	// Use this for initialization
	void Start () {
		mainCamera = Camera.main;
		canvasOn = false;
		bottomRightOn = 1.0f;
		this.transform.localScale = Vector3.one / 10.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Return)) {
			if (canvasOn) {
				this.transform.DOScale (Vector3.one / 10.0f, ANIMATION_SPEED);
				DOTween.To (() => bottomRightOn, x => bottomRightOn = x, 1.0f, 
					ANIMATION_SPEED);
				canvasOn = false;
			} else { // canvasOn is false
				this.transform.DOScale (Vector3.one, ANIMATION_SPEED);
				DOTween.To (() => bottomRightOn, x => bottomRightOn = x, 0.0f, 
					ANIMATION_SPEED);
				canvasOn = true;
			}
		}

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
}
