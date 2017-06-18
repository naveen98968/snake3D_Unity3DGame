using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// UIController.cs
/// 18/6/2017
/// naveensachdeva90@gmail.com
/// <para>
///game UI handling done here 
/// </para>
/// </summary>



public class UIController : MonoBehaviour {


	public RectTransform get_ready_Text;

	public Text score_text, timer; 


	public static int Total_Score;

	public GameObject GameOverScreen;


	public Text gameOver_score,gameOver_High_score;

	int highScore;

	float time_clock = 0;
	int minute =0;


	/// <summary>
	/// Use for Initializing values
	/// </summary>
	void Start () {
		
		InitialSetValue ();

		if(PlayerPrefs.HasKey("HighScore")){
			highScore = PlayerPrefs.GetInt ("HighScore");
		}
		else{
			PlayerPrefs.SetInt("HighScore",0);
			highScore = 0;
		}
		
	}


	/// <summary>
	/// Use for Initializing values
	/// </summary>
	void InitialSetValue(){
	 	snake3D.gameOverBool = false;
		Total_Score = 0;
		get_ready_Text.GetComponent<Text> ().color = new Color (1, 15/255f, 15/255, 1);
		LeanTween.alphaText (get_ready_Text, 0, 1);
		score_text.text =Total_Score.ToString();
		timer.text = "00:00";
	}
		

	public void UpdateScore(){

		score_text.text =Total_Score.ToString();

	}

	/// <summary>
	///game timer implementation 
	/// </summary>
	void Update(){
		if (!snake3D.gameOverBool) {
			time_clock += Time.deltaTime;
			if (time_clock > 59) {
				time_clock = 0;
				minute += 1;
			}
			int sec = (int)time_clock;
			timer.text = minute.ToString ("00") + ":" + sec.ToString ("00");
		}

	}

	/// <summary>
	///game Over Dialog controller
	/// </summary>
	public void GameOver(){
		
		GameOverScreen.SetActive (true);

		gameOver_score.text = Total_Score.ToString ();

		if (Total_Score > PlayerPrefs.GetInt ("HighScore")) {

			PlayerPrefs.SetInt ("HighScore", Total_Score);
			highScore = Total_Score;

		} else {
			highScore = PlayerPrefs.GetInt ("HighScore");

		}
		gameOver_High_score.text = highScore.ToString ();
		LeanTween.moveX (GameOverScreen.GetComponent<RectTransform>(), 0, 0.7f);

	}

	/// <summary>
	/// Replay button click event
	/// </summary>
	public void ReplayButtonEvent(){

		SceneManager.LoadScene ("snake3d");
	}
}
