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

	public float startTimer;
	public GameObject StartPanel;
	public Text startTimerText;


	// Use this for initialization
	void Awake () {
		isGameStarted = false;
		timeLeft = timeLimit;
		score = 0;
		holes = GameObject.FindGameObjectsWithTag ("hole");
		index = 0;
		minWait = 1.0f;
		maxWait = 10.0f;

		TimeText = GameObject.Find ("TimeText").GetComponent<Text> ();
		ScoreText = GameObject.Find ("ScoreText").GetComponent<Text> ();
		ScoreText.text = "0";

		startTimer = 3.0f;

		GameObject.FindGameObjectWithTag ("hand").GetComponent<HandScript> ().toggleAnimation (true);;
	}
	
	// Update is called once per frame
	void Update () {
		if (!isGameStarted) {
			if (startTimer >= 1.0f) {
				startTimerText.text = startTimer.ToString ("##");
			} else {
				startTimerText.text = "GO!";
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
		} else {
			score -= 10;
			soundEffect.clip = biteClip;
			soundEffect.Play ();
		}

		ScoreText.text = score.ToString();

	}
		
	public void MissedScore() {
		// Missed koi 
		soundEffect.clip = missClip;
		soundEffect.Play ();
		score -= 5;
		ScoreText.text = score.ToString();
	}

	private void EndGame() {
		// TODO
		SceneManager.LoadScene("EndScene");
	}


}
