using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour {

	// Use this for initialization
	void Start () {
		isPaused ();
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (0, 0.4f, 0);
	}

	public void isPaused() {
		if (PlayerPrefs.HasKey("Paused")) {
			string COINS = PlayerPrefs.GetString ("COINS");
			if (COINS.Contains(this.gameObject.name)) {
				Destroy (this.gameObject);
			}
		}
	}
}
