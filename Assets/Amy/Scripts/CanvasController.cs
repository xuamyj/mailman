using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CanvasController : MonoBehaviour {

	Camera mainCamera;
	bool canvasOn;
	float bottomRightOn;

	const float DIST_FROM_CAMERA = 18.0f;
	const float ANIMATION_SPEED = 0.5f;

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
				this.transform.DOScale (Vector3.one / 10.0f, ANIMATION_SPEED).SetEase (Ease.Linear);
				DOTween.To (() => bottomRightOn, x => bottomRightOn = x, 1.0f, 
					ANIMATION_SPEED).SetEase (Ease.Linear);
				canvasOn = false;
			} else { // canvasOn is false
				this.transform.DOScale (Vector3.one, ANIMATION_SPEED).SetEase (Ease.Linear);
				DOTween.To (() => bottomRightOn, x => bottomRightOn = x, 0.0f, 
					ANIMATION_SPEED).SetEase (Ease.Linear);
				canvasOn = true;
			}
		}

		Vector3 posOffset = mainCamera.transform.forward * DIST_FROM_CAMERA;
		
		// center, face us
		this.transform.position = mainCamera.transform.position + posOffset;
		this.transform.rotation = Quaternion.LookRotation (
			transform.position - mainCamera.transform.position);
		
		// move to bottom right as necessary using bottomRightOn
		Vector3 bottomRightOffset = this.transform.up * (-0.47f * DIST_FROM_CAMERA) + 
			this.transform.right * (0.85f * DIST_FROM_CAMERA);
		this.transform.position += bottomRightOffset * bottomRightOn;
	}
}
