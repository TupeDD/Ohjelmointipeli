using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Need : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void run() {
		if (!mapLoader.playingAgain) {
			gameObject.AddComponent<scoreToDB> ();
		} else {
			SceneManager.LoadScene ("Alkusivu");
		}
	}
}
