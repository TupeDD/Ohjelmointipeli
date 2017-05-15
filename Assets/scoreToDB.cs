using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class scoreToDB : MonoBehaviour {

	public GameObject inputField;
	private int pisteet;
	private string nimi;

	// Use this for initialization
	IEnumerator Start () {
		inputField = GameObject.FindGameObjectWithTag ("input");

		pisteet = Loppu.pisteet;
		nimi = inputField.GetComponent<Text>().text;

		if (nimi.Length > 0) {
			// Nimen puhdistus
			string pattern = @"[^a-zA-Z0-9 ]";
			string replacement = "";
			Regex rgx = new Regex (pattern);
			nimi = rgx.Replace (nimi, replacement);
			mapLoader.nimi = nimi;
			PlayerPrefs.SetString ("Pelaaja",nimi);

			WWW itemData = new WWW ("http://paja.esedu.fi/data14/tuomas.kiviranta/Ohjelmointipeli(Tietokanta)/index.php?security=NeverGonnaLetYouDownXD&function=postData&nimi=" + nimi + "&pisteet=" + pisteet);
			yield return itemData;
		} else {
			WWW itemData = new WWW ("http://paja.esedu.fi/data14/tuomas.kiviranta/Ohjelmointipeli(Tietokanta)/index.php?security=NeverGonnaLetYouDownXD&function=postData&pisteet="+pisteet);
			yield return itemData;
		}

		SceneManager.LoadScene ("Alkusivu");
		Destroy (this);

	}
	
	// Update is called once per frame
	void Update () {
	}
}
