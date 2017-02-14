using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Deplacement : MonoBehaviour {
	
	public float speed; //vitesse de déplacement
	Rigidbody rB;
	bool isDashing;

	AvatarBehavior aB;


	void Start () {
		rB = GetComponent<Rigidbody> ();
		aB = GetComponent <AvatarBehavior> ();
	}
	void Update () {
		// Gestion déplacement avatar, se sert de la postion relative de l'avatar
		float speedX = Input.GetAxis ("Horizontal") * (speed +(aB.stamina/3f)); // a ajuster (sans doute plus de vitesse pour vraiment marqué (et rewardé)) speed+(speed*aB.staminaValue)
		float speedY = Input.GetAxis ("Vertical") * (speed +(aB.stamina/3f));
		if (Input.GetAxis ("Horizontal") != 0 && isDashing == false) {
			transform.Translate ( speedX * Time.deltaTime,0, 0);
		}
		if (Input.GetAxis ("Vertical") != 0 && isDashing == false) {
			transform.Translate (0,0,  speedY * Time.deltaTime);
		}
		//
		/*if (Input.GetMouseButtonDown (0)||Input.GetButtonDown ("Dash")){
			Invoke ("StopVelocity", 0.65f);
			rB.AddForce (transform.up * 100f );
			//rB.AddForce (new Vector3(speedX, 0f, speedY) * 1500f);
			rB.AddForce (transform.forward * (1500f  aB.staminaValue));
			isDashing = true;

		}*/

	}
	void StopVelocity () {
		rB.velocity = Vector3.zero;
		rB.angularVelocity = Vector3.zero;
		isDashing = false;
	}
}
