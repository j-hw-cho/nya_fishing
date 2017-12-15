﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleController : MonoBehaviour {
	public int num;
	public bool isLeftHalf;

	private bool inited;		// Is initialized? (called from Game Script?)
	private bool amIActive;		// Fish popping up?

	private bool isKoi;			// Koi or Piranha


	private float nextGenTimer;
	private float timerMax;
	private float timerMin;

	private GameObject KoiPrefab;
	private GameObject PiranhaPrefab;
	private GameObject kTouchPrefab;
	private GameObject pTouchPrefab;

	// Use this for initialization
	void Start () {
		inited = false;
		amIActive = false;
		isKoi = false;

		nextGenTimer = 0.0f;
		timerMax = 10.0f;
		timerMin = 3.0f;

		KoiPrefab = Resources.Load ("Prefabs/koi") as GameObject;
		PiranhaPrefab = Resources.Load ("Prefabs/piranha") as GameObject;
		kTouchPrefab = isLeftHalf ? Resources.Load ("Prefabs/fish_catch2") as GameObject : Resources.Load ("Prefabs/fish_catch") as GameObject;
		pTouchPrefab = isLeftHalf ? Resources.Load ("Prefabs/fish_attack1") as GameObject : Resources.Load ("Prefabs/fish_attack2") as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (inited) {
			nextGenTimer -= Time.deltaTime;

			if (nextGenTimer <= 0) {
				activateHole();
			}
		}
	}

	public void Touched() {
		if (amIActive) {
		// TODO: Change the display
			bool isPlus = isKoi;
			GameObject.Find ("camera").GetComponent<GameScript> ().ChangeScore (isPlus);
			DeactivateHole(true);
		}
	}

	public void activateHole() {
		if (!amIActive) {
			System.Random rand = new System.Random();
			int generateKoi = rand.Next (4);
			GameObject myChild;
			if (generateKoi == 0) {
				// Generate Piranha
				myChild = GameObject.Instantiate(PiranhaPrefab);
				isKoi = false;

			} else {
				// Generate Koi
				myChild = GameObject.Instantiate(KoiPrefab);
				isKoi = true;

			}
			myChild.transform.parent = this.gameObject.transform;
			myChild.transform.localScale = new Vector3 (1f, 1f, 1f);
			myChild.transform.localPosition = new Vector3 (0f, -2.59f, 0);
		}
		if (!inited)
			inited = true;
		amIActive = true;
	}

	public void DeactivateHole(bool isCatched = false) {
		GameObject activeHole = this.gameObject.transform.GetChild (0).gameObject;
		if (activeHole != null)
			Destroy (activeHole);
	
		if (isCatched) {
			GameObject myChild = isKoi ? GameObject.Instantiate (kTouchPrefab) : GameObject.Instantiate (pTouchPrefab);
			myChild.transform.parent = this.gameObject.transform;
			if (isKoi) {
				myChild.transform.localPosition = isLeftHalf ? new Vector3 (0.64f, 0.97f, 0f) : new Vector3 (0f, 1f, 0f);
			} else {
				myChild.transform.localPosition = isLeftHalf ? new Vector3(2.77f, 2.31f, 0f) : new Vector3 (1.88f, 3.3f, 0f);
			}
			myChild.transform.localScale = new Vector3 (1f, 1f, 1f);
		} 

		amIActive = false;
		nextGenTimer = Random.Range (timerMin, timerMax);
		if (timerMin >= 1.0f) {
			timerMin -= 0.5f;
			timerMax -= 1.0f;
		}
	}
}
