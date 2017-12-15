using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameScript : MonoBehaviour {
	public bool isGameStarted;
	private float timeLimit = 60.0f;
	private float timeLeft;
	public static int score;
	public GameObject[] holes;

	public int index;

	float minWait;
	float maxWait;

	private Text ScoreText;
	private Text TimeText;

	public AudioSource soundEffect;

	public AudioClip catchClip;
	public AudioClip biteClip;
	public AudioClip missClip;
	public AudioClip timerClip;

	public float startTimer;
	public GameObject StartPanel;
	public Text startTimerText;
	private string StartTimerStr;

	private int combo;

	// Use this for initialization
	void Awake () {
		isGameStarted = false;
		timeLeft = timeLimit;
		score = 0;
		holes = GameObject.FindGameObjectsWithTag ("hole");
		index = 0;
		minWait = 1.0f;
		maxWait = 6.0f;

		TimeText = GameObject.Find ("TimeText").GetComponent<Text> ();
		ScoreText = GameObject.Find ("ScoreText").GetComponent<Text> ();
		ScoreText.text = "0";

		startTimer = 3.5f;

		combo = 0;

		GameObject.FindGameObjectWithTag ("hand").GetComponent<HandScript> ().toggleAnimation (true);;
	}
	
	// Update is called once per frame
	void Update () {
		if (!isGameStarted) {
			if (startTimer > 1.0f) {
				if (StartTimerStr != startTimer.ToString ("##")) {
					soundEffect.clip = timerClip;
					soundEffect.Play ();
					StartTimerStr = startTimer.ToString ("##"); 
					startTimerText.text = StartTimerStr;
				}
			} else {
				if (StartTimerStr != "GO!") {
					soundEffect.Play ();
					startTimerText.text = "GO!";

				}
			}

			startTimer -= Time.deltaTime;

			if (startTimer <= 0.0f) {
				StartPanel.SetActive (false);
				isGameStarted = true;
				GameObject.FindGameObjectWithTag ("hand").GetComponent<HandScript> ().toggleAnimation (false);

			}
		} else {
			timeLeft -= Time.deltaTime;
			TimeText.text = "Time Left: " + timeLeft.ToString ("##");

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
		if (isPlus) {
			score += 10;
			soundEffect.clip = catchClip;
			soundEffect.Play ();
			combo += 1;

			if (combo % 10 == 0) {
				score += 20;
			}

		} else {
			score -= 10;
			soundEffect.clip = biteClip;
			soundEffect.Play ();
			combo = 0;
		}

		ScoreText.text = score.ToString();

	}
		
	public void MissedScore() {
		// Missed koi 
		soundEffect.clip = missClip;
		soundEffect.Play ();
		score -= 5;
		ScoreText.text = score.ToString();
		combo = 0;
	}

	private void EndGame() {
		// TODO
		SceneManager.LoadScene("EndScene");
	}


}
