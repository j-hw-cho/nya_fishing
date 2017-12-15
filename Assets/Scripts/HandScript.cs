using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScript : MonoBehaviour {
	private bool isUsed;
	private float waitTime;
	private float waitTimeMax = 0.7f;
	private GameObject catEye;
	private int biteCount;
	public Animator myAnimator;

	// Use this for initialization
	void Awake () {
		isUsed = true;	
		waitTime = waitTimeMax;
		catEye =  GameObject.FindGameObjectWithTag ("eye");
		biteCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (!isUsed) { 
			waitTime -= Time.deltaTime;
			if (waitTime <= 0.0f) {
				// TODO: Activate hand
				isUsed = true;
				Color myColor = this.gameObject.GetComponent<SpriteRenderer> ().color;
				Color newColor = new Color (myColor.r, myColor.g, myColor.b, 1.0f);
				this.gameObject.GetComponent<SpriteRenderer> ().color = newColor;
			}
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
