using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AvatarBehavior : MonoBehaviour {

	public float stamina0;
	public float stamina1;
	public float stamina;
	public float maxStamina;

	public float decrementStamina;
	public float incrementStamina;
	public float staminaValue;
	float timerStamina;
	public float timerStaminaMax;
	Scene scene;
	/*public enum StateVisible
	{
		notVisible,
		slightlyVisible,
		visible
	}
	public StateVisible state;*/

	public Text staminaText;
	// Use this for initialization
	void Start () {
		scene = SceneManager.GetActiveScene ();
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
		/*if (stamina <= 0) {
			SceneManager.LoadScene (scene.name);
		}*/
		//stamina -= decrementStamina * Time.deltaTime;
		if (stamina > maxStamina) {
			stamina = maxStamina;
		}
		staminaText.text = Mathf.FloorToInt (stamina).ToString ();
	}
	void OnCollisionEnter (Collision col){
		if (col.gameObject.GetComponent<RessourceBehavior>()) {
			if (col.gameObject.GetComponent<RessourceBehavior> ().getTypeRessource() == 0){
				stamina0 += incrementStamina;
			}
			if (col.gameObject.GetComponent<RessourceBehavior> ().getTypeRessource() == 1){
				stamina1 += incrementStamina;
			}
			//Debug.Log (col.gameObject.GetComponent<RessourceBehavior> ().getTypeRessource ());
			stamina = stamina0 + stamina1;
			timerStamina = 0;
		}
		if (col.gameObject.GetComponent<EnnemiBehaviorV2>()) {
			stamina0 = 0;
			stamina1 = 1;
		}
	}
	void OnTriggerEnter (Collider col){
		if (col.gameObject.GetComponent<SignalBehavior>()) {
			//print ("suce mon chèvre"); 
		}
	}
}
