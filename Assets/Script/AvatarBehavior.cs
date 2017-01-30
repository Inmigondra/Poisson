using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvatarBehavior : MonoBehaviour {
	public float stamina;
	public float maxStamina;
	public float decrementStamina;
	public float incrementStamina;

	public enum StateVisible
	{
		notVisible,
		slightlyVisible,
		visible
	}
	public StateVisible state;

	public Text staminaText;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		stamina -= decrementStamina * Time.deltaTime;
		if (stamina > maxStamina) {
			stamina = maxStamina;
		}
		staminaText.text = stamina.ToString ();
	}
	void OnCollisionEnter (Collision col){
		if (col.gameObject.tag == "Ressource") {
			stamina += incrementStamina;
		}
	}
}
