using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour {
	public bool isGameStarted;
	private float timeLimit = 60.0f;
	private float timeLeft;
	public int score;
	public GameObject[] holes;

	public int index;

	float minWait;
	float maxWait;


	// Use this for initialization
	void Start () {
		isGameStarted = true;
		timeLeft = timeLimit;
		score = 0;
		holes = GameObject.FindGameObjectsWithTag ("hole");
		index = 0;
		minWait = 1.0f;
		maxWait = 10.0f;

	}
	
	// Update is called once per frame
	void Update () {
		if (isGameStarted) {
			timeLeft -= Time.deltaTime;


			if (timeLeft <= 0.0f) {
				isGameStarted = false;
				EndGame ();
			}

			foreach (GameObject hole in holes) {	// Activate holes
				IEnumerator coroutine = initHoles (hole);
				StartCoroutine (coroutine);
			}
		}

	}

	IEnumerator initHoles(GameObject hole) {
		float wait = Random.Range (minWait, maxWait);
		minWait += 0.7f;
		maxWait += 0.5f;

		yield return new WaitForSeconds(wait);

		hole.GetComponent<HoleController> ().activateHole ();


	}

	public void ChangeScore (bool isPlus) {
		if (isPlus)
			score += 10;
		else
			
			score -= 5;
	}

	private void EndGame() {
		// TODO
	}


}
