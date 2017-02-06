using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Deplacement : MonoBehaviour {
	
	public float speed; //vitesse de déplacement
	Rigidbody rb;
	bool isDashing;
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	void Update () {

		// Gestion déplacement avatar, se sert de la postion relative de l'avatar
		float speedX = Input.GetAxis ("Horizontal") * speed ;
		float speedY = Input.GetAxis ("Vertical") * speed ;
		if (Input.GetAxis ("Horizontal") != 0 && isDashing == false) {
			transform.Translate ( speedX * Time.deltaTime,0, 0);
		}
		if (Input.GetAxis ("Vertical") != 0 && isDashing == false) {
			transform.Translate (0,0,  speedY * Time.deltaTime);
		}
		//
		if (Input.GetMouseButtonDown (0)||Input.GetButtonDown ("Dash")){
			Invoke ("StopVelocity", 0.65f);
			rb.AddForce (transform.up * 100f );
			rb.AddForce (transform.forward * 1500f);
			isDashing = true;
			//
		}

	}
	void StopVelocity () {
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
		isDashing = false;
	}
}
