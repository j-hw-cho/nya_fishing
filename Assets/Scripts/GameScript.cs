using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour {
	public bool isGameStarted;
	private float timeLimit = 60.0f;
	private float timeLeft;
	public int score;
	public GameObject[] holes;



	// Use this for initialization
	void Start () {
		isGameStarted = true;
		timeLeft = timeLimit;
		score = 0;
		holes = GameObject.FindGameObjectsWithTag ("hole");
	}
	
	// Update is called once per frame
	void Update () {
		if (isGameStarted) {
			timeLeft -= Time.deltaTime;

			if (timeLeft <= 0.0f) {
				isGameStarted = false;
				EndGame ();
			}

		}

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
