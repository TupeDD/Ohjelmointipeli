﻿using System.Collections;
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

	public void Awake()
	{
		/*DontDestroyOnLoad(this);

		if (FindObjectsOfType(GetType()).Length > 1)
		{
			Destroy(gameObject);
		}*/
	}

	// Use this for initialization
	void Start () {
		/*Button m1 = Map1.GetComponent<Button> ();
		m1.onClick.AddListener (map1);
		Button m2 = Map2.GetComponent<Button> ();
		m2.onClick.AddListener (map2);
		Button m3 = Map3.GetComponent<Button> ();
		m3.onClick.AddListener (map3);*/
	}
	
	// Update is called once per frame
	void Update () {
		if (SceneManager.GetActiveScene().name == "Alkusivu") {
			if (Map1 == null) {
				Map1 = GameObject.FindGameObjectWithTag ("map1");
			}
			if (Map2 == null) {
				Map2 = GameObject.FindGameObjectWithTag ("map2");
			}
			if (Map3 == null) {
				Map3 = GameObject.FindGameObjectWithTag ("map3");
			}

			if (mapsWon == 0) {
				Map1.GetComponent<Button> ().interactable = true;
			} 
			else if (mapsWon == 1) {
				/*if (!reload) {
					Button m1 = Map1.GetComponent<Button> ();
					m1.onClick.AddListener (map1);
					Button m2 = Map2.GetComponent<Button> ();
					m2.onClick.AddListener (map2);
					Button m3 = Map3.GetComponent<Button> ();
					m3.onClick.AddListener (map3);
					reload = true;
				}*/
				Map1.GetComponent<Button> ().interactable = true;
				Map2.GetComponent<Button> ().interactable = true;
			} 
			else {
				/*reload = false;
				if (!reload) {
					Button m1 = Map1.GetComponent<Button> ();
					m1.onClick.AddListener (map1);
					Button m2 = Map2.GetComponent<Button> ();
					m2.onClick.AddListener (map2);
					Button m3 = Map3.GetComponent<Button> ();
					m3.onClick.AddListener (map3);
					reload = true;
				}*/
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
}
