using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Peli : MonoBehaviour {

	// Liiku paneelin askeleet ja suunta
	public GameObject liiku_maara;
	public GameObject suuntaText;

	// Kamerat ja Canvasit
	public Camera fps_cam;
	public Camera main_cam;
	public Camera mini_cam;
	public Canvas mainCanvas;
	public Canvas fpsCanvas;

	// Pelaaja
	public GameObject pelaaja;

	// Pisteet
	public static int coins = 0;
	public static int deaths;
	public static int rounds;
	public static int roundActions;
	public int coinsToWin = 4;

	// Unityn FirstPersonController scripti pelaajassa
	public UnityStandardAssets.Characters.FirstPerson.FirstPersonController fps;

	// Pelaajan koordinaatit ja suunta
	public float x;
	public float x_kopio;
	public float z;
	public float z_kopio;
	public float rot_y;
	public float temp_rot_y;
	public string suunta = "up";
	public int askeleet;
	public float turnspeed = 1f;
	public string ksuunta;
	public bool Liiku = false;
	public bool Kaanny = false;

	public float temp_rot = 0;
	private Vector3 originalPosition;
	private Quaternion originalRotation;
	private Quaternion orgCam;

	public static List<string> TOIMINTA;
	public static List<string> TOIMINTA2;
	public int Laskin = 0;
	public bool waiting = false;
	public int suuntaNumero = 0;
	public static bool suorita = false;
	public bool die = false;

	public GameObject dirLight;
	public GameObject Coin;
	public GameObject canvasCoins;
	public GameObject fpsCoins;

	// Frost
	private float freeze = 0f;
	private bool closeToFire = false;
	private bool freezeOn;

	// Äänet
	public AudioClip collect;
	public AudioClip burn;
	public AudioClip vic1;
	public AudioClip vic2;
	public AudioClip vic3;
	public AudioClip lose;
	AudioSource Audio;

	// Tykit Terrain3
	public GameObject can1;
	public GameObject can2;
	public GameObject can3;

	public GameObject deathScreen;
	public GameObject winScreen;
	public GameObject infoScreen;
	public GameObject infoText;

	// Use this for initialization
	void Start () {
		Audio = GetComponent<AudioSource>();
		coinsToWin = GameObject.FindGameObjectsWithTag ("Coin").Length;
		TOIMINTA = new List<string> ();
		TOIMINTA2 = new List<string> ();
		originalPosition = new Vector3 (4f, 2f, 3.5f);
		originalRotation = pelaaja.transform.rotation;
		orgCam = pelaaja.GetComponentInChildren<Camera> ().transform.rotation;
		freezeOn = GetComponentInChildren<FrostEffect> ().enabled;
		coins = 0;
		string info = "";
		if (mapLoader.mapNum == 1) {
			info = "Tehtävänä on kerätä kaikki kolikot mahdollisimman vähillä kierroksilla ja varoa vaarallisia luonnon ilmiöitä";
			infoText.GetComponent<Text> ().text = info;
		} else if (mapLoader.mapNum == 2) {
			info = "Talvella on kylmä ja kylmyyttä ehkäiset nuotion seurassa";
			infoText.GetComponent<Text> ().text = info;
		} else {
			info = "Ritarit ovat ladanneet tykkeja sotaa varten ja ne voivat laueta itsestään jos menet niiden eteen";
			infoText.GetComponent<Text> ().text = info;
		}
	}
	
	// Update is called once per frame
	void Update () {
		canvasCoins.GetComponent<Text> ().text = coins + "";
		fpsCoins.GetComponent<Text> ().text = coins + "";

		// Jäätyminen
		if (freezeOn && mapLoader.mapNum == 2) {
			if (freeze >= 0.6f) {
				Die ();
			}
			if (closeToFire) {
				if (freeze > 0) {
					freeze -= 0.001f;
				}
			} else if (suorita) {
				if (freeze < 0.6f) {
					freeze += 0.0001f;
				}
			}
			GetComponentInChildren<FrostEffect> ().FrostAmount = freeze;
		}

		if (suorita) {
			if (TOIMINTA.Count > 0 && !waiting && Laskin + 1 <= TOIMINTA.Count) {
				if (TOIMINTA [Laskin] == "liiku") {
					waiting = true;
					gameObject.SendMessage (TOIMINTA [Laskin], int.Parse (TOIMINTA2 [Laskin]));
					Laskin++;
				} else {
					waiting = true;
					gameObject.SendMessage (TOIMINTA [Laskin], TOIMINTA2 [Laskin]);
					Laskin++;
				}
			}

			if (Liiku) {
				if (suunta == "up") {
					if (z_kopio >= z + askeleet) {
						Liiku = false;
						fps.Vertical = 0;

						waiting = false;
						if (Laskin >= TOIMINTA.Count) {
							TOIMINTA.Clear ();
							TOIMINTA2.Clear ();
						}
					} else {
						z_kopio = pelaaja.transform.position.z;
					}
				} else if (suunta == "down") {
					if (z_kopio <= z - askeleet) {
						Liiku = false;
						fps.Vertical = 0;

						waiting = false;
						if (Laskin >= TOIMINTA.Count) {
							TOIMINTA.Clear ();
							TOIMINTA2.Clear ();
						}
					} else {
						z_kopio = pelaaja.transform.position.z;
					}
				} else if (suunta == "left") {
					if (x_kopio <= x - askeleet) {
						Liiku = false;
						fps.Vertical = 0;

						waiting = false;
						if (Laskin >= TOIMINTA.Count) {
							TOIMINTA.Clear ();
							TOIMINTA2.Clear ();
						}
					} else {
						x_kopio = pelaaja.transform.position.x;
					}
				} else if (suunta == "right") {
					if (x_kopio >= x + askeleet) {
						Liiku = false;
						fps.Vertical = 0;

						waiting = false;
						if (Laskin >= TOIMINTA.Count) {
							TOIMINTA.Clear ();
							TOIMINTA2.Clear ();
						}
					} else {
						x_kopio = pelaaja.transform.position.x;
					}
				}
			}

			if (Kaanny) {
				if (ksuunta == "vasen") {
					if (temp_rot == 0) {
						Kaanny = false;
						waiting = false;
						vaihdaSuunta (-1);
						if (Laskin >= TOIMINTA.Count) {
							TOIMINTA.Clear ();
							TOIMINTA2.Clear ();
						}
					} else {
						temp_rot -= 0.5f;
						pelaaja.transform.Rotate (0, -0.5f, 0);
					}	
				} else if (ksuunta == "oikea") {
					if (temp_rot == 0) {
						Kaanny = false;
						waiting = false;
						vaihdaSuunta (1);
						if (Laskin >= TOIMINTA.Count) {
							TOIMINTA.Clear ();
							TOIMINTA2.Clear ();
						}
					} else {
						temp_rot -= 0.25f;
						pelaaja.transform.Rotate (0, 0.25f, 0);
					}
				}
			}
			if (TOIMINTA.Count == 0 && TOIMINTA2.Count == 0 && !waiting) {
				endRound ();
			}
		}
		if (die) {
			double c = -90.0 / -0.05;
			double cc = 0.0;
			if (cc == c) {
				die = false;
			} 
			else {
				cc++;
				pelaaja.GetComponentInChildren<Camera> ().transform.Rotate (0, 0, -0.025f);
			} 
		}
	}

	public void kameranVaihto() {
		if (infoScreen.activeSelf) {
			infoScreen.SetActive (false);
		}
		if (main_cam.enabled) 
		{
			main_cam.enabled = false;
			fps_cam.GetComponent<Camera> ().targetDisplay = 0;
			mainCanvas.enabled = false;
			fpsCanvas.GetComponent<Canvas> ().targetDisplay = 0;
		}
		else 
		{
			fps_cam.GetComponent<Camera> ().targetDisplay = 1;
			fpsCanvas.GetComponent<Canvas> ().targetDisplay = 1;
			mainCanvas.enabled = true;
			main_cam.enabled = true;
		}
	}

	public void liiku(int montako) {
		if (!mapLoader.playingAgain) {
			roundActions++;
		}
		Liiku = true;
		askeleet = montako*4;
		x = pelaaja.transform.position.x;
		x_kopio = x;
		z = pelaaja.transform.position.z;
		z_kopio = z;
		fps.Vertical = 1f;
		
	}

	public void vaihdaSuunta(int Numero) {
		suuntaNumero += Numero;
		if (suuntaNumero < -1) {
			suuntaNumero = 2;
		}
		if (suuntaNumero > 2) {
			suuntaNumero = -1;
		}
		if (suuntaNumero == 0) {
			suunta = "up";
		} else if (suuntaNumero == 1) {
			suunta = "right";
		} else if (suuntaNumero == 2) {
			suunta = "down";
		} else if (suuntaNumero == -1) {
			suunta = "left";
		}
		if (pelaaja.transform.rotation.y == 0) {
			suunta = "up";
		} else if (pelaaja.transform.rotation.y == 1) {
			suunta = "down";
		}
	}

	public void kaanny(string Suunta) {
		if (!mapLoader.playingAgain) {
			roundActions++;
		}
		temp_rot += 90f;
		Kaanny = true;
		ksuunta = Suunta;
		rot_y = pelaaja.transform.eulerAngles.y;
		temp_rot_y = pelaaja.transform.eulerAngles.y;
	}

	public void minus() {
		int luku = int.Parse (liiku_maara.GetComponent<Text> ().text);
		if (luku > 1) {
			luku -= 1;
			liiku_maara.GetComponent<Text> ().text = "" + luku;
		}
	}

	public void plus() {
		int luku = int.Parse(liiku_maara.GetComponent<Text>().text);
		if (luku < 99) {
			luku += 1;
			liiku_maara.GetComponent<Text> ().text = "" + luku;
		}
	}

	public void vaihdaSuunta() {
		if (suuntaText.GetComponent<Text>().text == "Vasen") {
			suuntaText.GetComponent<Text> ().text = "Oikea";
		} else {
			suuntaText.GetComponent<Text> ().text = "Vasen";
		}
	}

	public void endRound() {
		Laskin = 0;
		suorita = false;
		waiting = false;
		dirLight.GetComponent<SceneController> ().emptyToiminnot();
		Invoke("kameranVaihto", 0.5f);
	}

	public void restart() {
		die = false;
		Audio.Stop ();
		deathScreen.gameObject.SetActive (false);
		Laskin = 0;
		suorita = false;
		waiting = false;
		dirLight.GetComponent<SceneController> ().emptyToiminnot();
		kameranVaihto();
		pelaaja.transform.rotation = originalRotation;
		pelaaja.transform.position = originalPosition;
		pelaaja.GetComponentInChildren<Camera> ().transform.rotation = orgCam;
		Audio.Play ();
	}

	public void Die() {
		Audio.PlayOneShot (lose, 0.8f);
		deathScreen.gameObject.SetActive (true);
		if (!mapLoader.playingAgain) {
			deaths++;
		}
		TOIMINTA.Clear ();
		TOIMINTA2.Clear ();
		fps.Vertical = 0;
		suunta = "up";
		suuntaNumero = 0;
		suorita = false;
		Liiku = false;
		askeleet = 0;
		Kaanny = false;
		die = true;
	}
		
	public void win() {
		coins = 0;
		TOIMINTA.Clear ();
		TOIMINTA2.Clear ();
		fps.Vertical = 0;
		suorita = false;

		if (mapLoader.mapNum == 1) {
			Audio.PlayOneShot (vic1, 1f);
		} else if (mapLoader.mapNum == 2) {
			Audio.PlayOneShot (vic2, 0.9f);
		} else {
			Audio.PlayOneShot (vic3, 0.8f);
		}
		winScreen.SetActive (true);
	}

	public void winButtonFunction() {
		sceneLoad ();
		winScreen.SetActive (false);
	}

	void sceneLoad() {
		string scene;
		if (mapLoader.mapNum == 3) {
			scene = "Loppu";
		} else {
			scene = "Alkusivu";
		}
		mapLoader.mapWon();
		SceneManager.LoadScene(scene);
	}

	public void infoSulje() {
		infoScreen.SetActive (false);
	}

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag == "border" && !Kaanny && Liiku) {
			string lupasuunta = "";

			if (col.gameObject.name == "Border1") {
				lupasuunta = "left";
			}
			else if (col.gameObject.name == "Border2") {
				lupasuunta = "right";
			}
			else if (col.gameObject.name == "Border3") {
				lupasuunta = "up";
			}
			else if (col.gameObject.name == "Border4") {
				lupasuunta = "down";
			}

			if (suunta != lupasuunta) {
				fps.Vertical = 0;
				Liiku = false;
				waiting = false;
				if (Laskin >= TOIMINTA.Count) {
					TOIMINTA.Clear ();
					TOIMINTA2.Clear ();
				}
			}
		}
	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "Coin" && suorita && Liiku) {
			Destroy (col.gameObject);
			Audio.PlayOneShot (collect, 0.7f);
			coins = 4;
			//coins++;
			if (coins == coinsToWin) {
				win ();
			}
		}
		if (col.gameObject.tag == "Lava") {
			Audio.PlayOneShot (burn, 0.5f);
			Die ();
		}
		if (col.gameObject.tag == "Fire") {
			closeToFire = true;
		}
		if (col.gameObject.tag == "cannon") {
			col.gameObject.transform.parent.gameObject.transform.Find("cannon ball pref").gameObject.SetActive(true);
			col.gameObject.transform.parent.gameObject.transform.GetChild (6).gameObject.SetActive (true);
			col.gameObject.transform.parent.gameObject.transform.Find("cannon ball pref").gameObject.GetComponent<explosion>().explode();
			if (explosion.EXPLODE) {
				Die ();
			}
		}
	}
	void OnTriggerExit(Collider col) {
		if (col.gameObject.tag == "Fire") {
			closeToFire = false;
		}
	}

}
