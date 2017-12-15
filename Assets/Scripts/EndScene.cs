using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScene : MonoBehaviour {
	private int score;

	// Use this for initialization
	void Awake () {
		score = GameScript.score;
		int oldHighscore = PlayerPrefs.GetInt ("highscore", 0);
		if (score > oldHighscore) {

			GameObject.Find ("ScoreText").GetComponent<Text> ().text = "Score: " + score.ToString () +"\nNew High Score!!";		
			PlayerPrefs.SetInt ("highscore", score);
			PlayerPrefs.Save ();
		} else {
			GameObject.Find ("ScoreText").GetComponent<Text> ().text = "Score: " + score.ToString () + "\nHigh Score: " + oldHighscore.ToString();
		}
	}
	
	public void replayClick() {
		SceneManager.LoadScene("GameScene");
	}
}
