using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loppu : MonoBehaviour {

	private int kierrokset;
	private int liikkeet;
	public static int pisteet;
	private int kuolemat;

	public GameObject Kierrokset;
	public GameObject Liikkeet;
	public GameObject Pisteet;
	public GameObject Kuolemat;

	// Use this for initialization
	void Start () {
		kierrokset = Peli.rounds;
		liikkeet = Peli.roundActions;
		/* 100 pistettä saa jos 
		 Map 1. Rounds = 3 & roundActions = 11
		 Map 2. Rounds = 3 & roundActions = 13
		 Map 3. Rounds = 2 & roundActions = 9
		*/
		pisteet = 141 - liikkeet - kierrokset - (kuolemat * 10);
		if (pisteet < 0) {
			pisteet = 0;
		}
		kuolemat = Peli.deaths;

		Kierrokset.GetComponent<Text> ().text = "Kierrokset : " + kierrokset;
		Liikkeet.GetComponent<Text> ().text = "Liikkeet : " + liikkeet;
		Pisteet.GetComponent<Text> ().text = "Pisteet : " + pisteet;
		Kuolemat.GetComponent<Text> ().text = "Kuolemat : " + kuolemat;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
