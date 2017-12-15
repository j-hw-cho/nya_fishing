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

	private GameObject KoiPrefab;
	private GameObject PiranhaPrefab;
	private GameObject kTouchPrefab;
	private GameObject pTouchPrefab;

	private int fadeOutFrame;
	private bool fadeOut;
	private int frameNo;
	private float alphaChangeUnit;

	private bool ExitState;

	// Use this for initialization
	void Awake () {
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

		frameNo = 20;
		fadeOutFrame = 0;
		fadeOut = false;
		alphaChangeUnit = 1f / (float)frameNo;
		ExitState = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (inited) {
			if (fadeOut) {
				GameObject myChild = this.gameObject.transform.GetChild (0).gameObject;
				Color childColor = myChild.GetComponent<SpriteRenderer> ().color;
				float a = childColor.a;
				Color newColor = new Color (childColor.r, childColor.g, childColor.b, a - alphaChangeUnit);
				myChild.GetComponent<SpriteRenderer> ().color = newColor;

				fadeOutFrame += 1;

				if (fadeOutFrame >= frameNo) {
					Destroy (myChild);
					fadeOut = false;
					fadeOutFrame = 0;
					if (!isKoi) {
						GameObject.FindGameObjectWithTag ("hand").GetComponent<HandScript> ().changeEye (true);
					}
					ResetForNext ();
				}

			} else if (amIActive) {
				Animator am = this.gameObject.gameObject.transform.GetChild (0).gameObject.GetComponent<Animator> ();
				if (am != null) {
					if (isKoi) {
						if (am.GetCurrentAnimatorStateInfo (0).IsName ("koi_exit")) {
							ExitState = true;
						} else {
							if (ExitState) {
								// End of Animation
								Debug.Log ("Animation Done!");
								DeactivateHole ();
								// Mark Missed
								GameObject.Find("camera").GetComponent<GameScript>().MissedScore();
							}
							ExitState = false;
						}
					} else {		// Piranha
						if (am.GetCurrentAnimatorStateInfo (0).IsName ("piranah_exit")) {
							ExitState = true;
						} else {
							if (ExitState) {
								// End of Animation
								Debug.Log ("Animation Done!");
								DeactivateHole ();
							}
							ExitState = false;
						}

					}

				}

			} else {
				nextGenTimer -= Time.deltaTime;

				if (nextGenTimer <= 0 && !amIActive) {
					activateHole ();
				}
			}
		}
	}

	public void Touched() {
		if (amIActive) {
		// TODO: Change the display
			bool isPlus = isKoi;
			GameObject.Find ("camera").GetComponent<GameScript> ().ChangeScore (isPlus);
			GameObject.FindGameObjectWithTag ("hand").GetComponent<HandScript> ().handHide ();
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
		ExitState = false;
		if (activeHole != null)
			Destroy (activeHole);
		
	
		if (isCatched) {
			GameObject myChild = isKoi ? GameObject.Instantiate (kTouchPrefab) : GameObject.Instantiate (pTouchPrefab);
			myChild.transform.parent = this.gameObject.transform;
			if (isKoi) {
				myChild.transform.localPosition = isLeftHalf ? new Vector3 (0.64f, 0.97f, 0f) : new Vector3 (0f, 1f, 0f);
			} else {
				GameObject.FindGameObjectWithTag ("hand").GetComponent<HandScript> ().changeEye (false);
				myChild.transform.localPosition = isLeftHalf ? new Vector3 (2.77f, 2.31f, 0f) : new Vector3 (1.88f, 3.3f, 0f);
			}
			myChild.transform.localScale = new Vector3 (1f, 1f, 1f);
			fadeOut = true;
			fadeOutFrame = 0;
		} else {
			ResetForNext ();
		}


	}

	private void ResetForNext() {
		amIActive = false;
		nextGenTimer = Random.Range (timerMin, timerMax);
		if (timerMin >= 1.0f) {
			timerMin -= 0.5f;
			timerMax -= 1.0f;
		}

	}
		
}
