using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScript : MonoBehaviour {
	private bool isUsed;
	private float waitTime;
	private float waitTimeMax = 0.7f;
	private GameObject catEye;
	private bool isTouching;
	private int biteCount;
	public Animator myAnimator;

	private float eyeMoveUnit = 0.02f;
	private float eyeMoveSign = 1;

	private bool isGameStarted;

	// Use this for initialization
	void Awake () {
		isUsed = true;	
		waitTime = waitTimeMax;
		catEye =  GameObject.FindGameObjectWithTag ("eye");
		biteCount = 0;
		isTouching = false;
		isGameStarted = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!isGameStarted)
			isGameStarted = GameObject.Find ("camera").GetComponent<GameScript> ().isGameStarted;

		if (!isUsed) { 
			waitTime -= Time.deltaTime;
			if (waitTime <= 0.0f) {
				// TODO: Activate hand
				isUsed = true;
				Color myColor = this.gameObject.GetComponent<SpriteRenderer> ().color;
				Color newColor = new Color (myColor.r, myColor.g, myColor.b, 1.0f);
				this.gameObject.GetComponent<SpriteRenderer> ().color = newColor;
			}
		} else if (isGameStarted) {
			// Eye movement
			Vector3 eyePos = catEye.transform.localPosition;
			float newX = eyePos.x + eyeMoveUnit * eyeMoveSign;
			if (newX > 0.3f || newX < -0.3f) {
				eyeMoveSign = eyeMoveSign * -1;
			}

			catEye.transform.localPosition = new Vector3 (eyePos.x + eyeMoveUnit * eyeMoveSign, eyePos.y, eyePos.z);
		}
	}

	public void toggleAnimation(bool stop) {
		if (stop)
			myAnimator.speed = 0.0f;
		else
			myAnimator.speed = 1.0f;
	}

	public void handHide() {
		if (isUsed) {
			isUsed = false;
			waitTime = waitTimeMax;
			Color myColor = this.gameObject.GetComponent<SpriteRenderer> ().color;
			Color newColor = new Color (myColor.r, myColor.g, myColor.b, 0.0f);
			this.gameObject.GetComponent<SpriteRenderer> ().color = newColor;
		}

	}

	public void moveEye(int groupNo) {
		if (groupNo == -1) {
			// Normal Eye
			catEye.transform.localPosition = new Vector3 (0f, 0f, 0f);
		} else if (groupNo == 0 || groupNo == 4) {
			catEye.transform.localPosition = new Vector3 (-0.3f, 0f, 0f);
		} else if (groupNo == 1 || groupNo == 5) {
			catEye.transform.localPosition = new Vector3 (-0.15f, 0f, 0f);
		} else if (groupNo == 2 || groupNo == 6) {
			catEye.transform.localPosition = new Vector3 (0.15f, 0f, 0f);
		} else {
			catEye.transform.localPosition = new Vector3 (0.3f, 0f, 0f);
		}
	}

	public void changeEye(bool isNormalEye) {
		if (isNormalEye) {
			biteCount -= 1;
			if (biteCount <= 0)
				catEye.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Cat/eye");
		} else {
			biteCount += 1;
			catEye.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Cat/eye_hurt");
		}
	}
		
}
