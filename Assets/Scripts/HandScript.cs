using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScript : MonoBehaviour {
	private bool isUsed;
	private float waitTime;
	private float waitTimeMax = 0.7f;

	// Use this for initialization
	void Start () {
		isUsed = true;	
		waitTime = waitTimeMax;
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

	public void handHide() {
		if (isUsed) {
			isUsed = false;
			waitTime = waitTimeMax;
			Color myColor = this.gameObject.GetComponent<SpriteRenderer> ().color;
			Color newColor = new Color (myColor.r, myColor.g, myColor.b, 0.0f);
			this.gameObject.GetComponent<SpriteRenderer> ().color = newColor;
		}

	}
}
