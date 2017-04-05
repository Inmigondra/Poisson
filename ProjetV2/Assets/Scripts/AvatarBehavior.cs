using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AvatarBehavior : MonoBehaviour {

	public float stamina;
	public float maxStamina;

	public float decrementStamina;
	public float incrementStamina;
	public float staminaValue;
	public float timerStamina;
	public float timerStaminaMax;
	public bool immediateDeath = true; //Sers pour le prototypage
	Text staminaText;
	GameObject gamemanagerPrefab;
	GameObject textForStamina;
	GameManager gM;
	Deplacement dP;
	// Use this for initialization
	void Awake (){
		// LEs deux objets cherchés doivent toujours s'appeler de la même façon
		gamemanagerPrefab = GameObject.Find ("GameManager");
		textForStamina = GameObject.Find ("Text_Stamina");
	}
	void Start () {
		staminaText = textForStamina.GetComponent <Text> ();
		gM = gamemanagerPrefab.GetComponent <GameManager>();
		dP = GetComponent <Deplacement> ();
	}
	
	// Update is called once per frame
	void Update () {
		timerStamina += 1 * Time.deltaTime;
		if (timerStamina >= timerStaminaMax) {
			stamina -= decrementStamina;
			timerStamina = 0;
			if (stamina <= 0) {
				stamina = 0;
			}
		}
		staminaValue = stamina / maxStamina;
		if (stamina > maxStamina) {
			stamina = maxStamina;
		}
		staminaText.text = Mathf.FloorToInt (stamina).ToString ();
	}
	void OnCollisionEnter (Collision col){
		if (col.gameObject.tag == "Ressource") {
			stamina += incrementStamina;
			timerStamina = 0;
			gM.actualNumberRessources += 1;
		}
		if (col.gameObject.tag == "Ennemi") {
			if (immediateDeath){
				gM.GameOver ();
			}
		}
		if (col.gameObject.tag == "Arrive") {
			gM.NextLevel ();
		}
	}
	void OnTriggerEnter (Collider col){
		if (col.gameObject.GetComponent<SignalBehavior>()) {
			//print ("suce mon chèvre"); 
		}
	}
}
