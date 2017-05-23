using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Database : MonoBehaviour {

	public string[] items;
	private string nimi;

	// Use this for initialization
	IEnumerator Start () {
		nimi = mapLoader.nimi;
		if (nimi == "") {
			if (PlayerPrefs.HasKey("Pelaaja")) {
				nimi = PlayerPrefs.GetString ("Pelaaja");
			}
		}
		this.gameObject.GetComponent<Text> ().text = "";

		WWW itemData = new WWW ("http://paja.esedu.fi/data14/tuomas.kiviranta/Ohjelmointipeli(Tietokanta)/index.php?security=NeverGonnaLetYouDownXD&function=getData");
		yield return itemData;
		string itemDataString = itemData.text;

		items = itemDataString.Split (';');

		string color = "";
		string colorEnd = "";
		foreach (string item in items) {
			if (item.Length > 0) {
				if (item.Contains (nimi) && nimi != "") {
					color = "<color=#008000ff>";
					colorEnd = "</color>";
				}
				this.gameObject.GetComponent<Text> ().text += (color+item+colorEnd+"\n");
				color = "";
				colorEnd = "";
			}

		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
