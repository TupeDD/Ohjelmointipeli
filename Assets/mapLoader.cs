using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mapLoader : MonoBehaviour {

	public GameObject Map1;
	public GameObject Map2;
	public GameObject Map3;
	public static int mapNum;
	public static int mapsWon;
	private bool reload = false;
	public static bool playingAgain = false;
	public static string nimi = "";
	public static bool muted = false;


	// Use this for initialization
	void Start () {
		if (!Screen.fullScreen) {
			Screen.fullScreen = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (SceneManager.GetActiveScene().name == "Alkusivu") {
			if (mapsWon == 0) {
				Map1.GetComponent<Button> ().interactable = true;
			} 
			else if (mapsWon == 1) {
				Map1.GetComponent<Button> ().interactable = true;
				Map2.GetComponent<Button> ().interactable = true;
			} 
			else {
				Map1.GetComponent<Button> ().interactable = true;
				Map2.GetComponent<Button> ().interactable = true;
				Map3.GetComponent<Button> ().interactable = true;
			}
		}
	}
	public void map1() {
		if (mapsWon >= 1) {
			playingAgain = true;
		} else {
			playingAgain = false;
		}
		mapNum = 1;
		SceneManager.LoadScene ("Peli");
	}
	public void map2() {
		if (mapsWon >= 2) {
			playingAgain = true;
		} else {
			playingAgain = false;
		}
		mapNum = 2;
		SceneManager.LoadScene ("Peli");
	}
	public void map3() {
		if (mapsWon == 3) {
			playingAgain = true;
		} else {
			playingAgain = false;
		}
		mapNum = 3;
		SceneManager.LoadScene ("Peli");
	}

	public static void mapWon() {
		if (mapNum == mapsWon+1) {
			mapsWon++;
		} 
	}

	public void EXIT() {
		Application.Quit ();
	}
	public void MUTE() {
		if (muted) {
			UnityEngine.AudioListener.pause = false;
			muted = false;
		}
		else {
			UnityEngine.AudioListener.pause = true;
			muted = true;
		}
	}
}
