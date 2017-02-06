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
		staminaValue = stamina / maxStamina;
		if (stamina <= 0) {
			SceneManager.LoadScene (scene.name);
		}
		stamina -= decrementStamina * Time.deltaTime;
		if (stamina > maxStamina) {
			stamina = maxStamina;
		}
		staminaText.text = Mathf.FloorToInt (stamina).ToString ();
	}
	void OnCollisionEnter (Collision col){
		if (col.gameObject.tag == "Ressource") {
			stamina += incrementStamina;
		}
		if (col.gameObject.tag == "Ennemi") {
			stamina -= decrementStamina;
		}
	}
}
