using System.Collections;
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

	// Use this for initialization
	void Start () {
		inited = false;
		amIActive = false;
		isKoi = false;

		nextGenTimer = 0.0f;
		timerMax = 10.0f;
		timerMin = 3.0f;
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
		}
	}

	public void activateHole() {
		if (!amIActive) {
			System.Random rand = new System.Random();
			int generateKoi = rand.Next (4);
			if (generateKoi == 0) {
				// Generate Piranha
			} else {
				// Generate Koi
			}
		}
		if (!inited)
			inited = true;
	}

	public void DeactivateHole(bool isCatched = false) {
		if (!isCatched) {

		} else {

		}

		amIActive = false;
		nextGenTimer = Random.Range (timerMin, timerMax);
		if (timerMin >= 1.0f) {
			timerMin -= 0.5f;
			timerMax -= 1.0f;
		}


	}
}
