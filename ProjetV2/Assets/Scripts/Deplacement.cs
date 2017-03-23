using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Deplacement : MonoBehaviour {
	
	public float speed; //vitesse de déplacement
	Rigidbody rB;
	bool isDashing;
	public float diviseur = 1f; //donne la valeur de division par rapport à la stamina
	AvatarBehavior aB;


	void Start () {
		rB = GetComponent<Rigidbody> ();
		aB = GetComponent <AvatarBehavior> ();
	}
	void Update() {
		
		float speedX = Input.GetAxis ("Horizontal") * (speed +(aB.stamina/diviseur)); // a ajuster (sans doute plus de vitesse pour vraiment marqué (et rewardé)) speed+(speed*aB.staminaValue)
		float speedY = Input.GetAxis ("Vertical") * (speed + (aB.stamina / diviseur));
		if (Input.GetAxis ("Horizontal") != 0 || Input.GetAxis ("Vertical") != 0) {
			//transform.Translate (speedX, 0, 0);
			rB.velocity = (gameObject.transform.right* speedX) + (gameObject.transform.forward * speedY) + (gameObject.transform.up * -1f) ;
		}
	}

	void StopVelocity () {
		rB.velocity = Vector3.zero;
		rB.angularVelocity = Vector3.zero;
		isDashing = false;
	}
	public void ContactEnnemi (GameObject ennemi) {
		Vector3 dir = (transform.position - ennemi.transform.position);
		rB.AddForce (dir * 1000f);
		Debug.Log (dir);
	}
}
