using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScriptV2 : MonoBehaviour {
	public float sensitivity ;
	public GameObject targetOrbit;
	public GameObject cameraMain;
	CameraCollider cC;
	// Use this for initialization
	void Start () {
		cC = cameraMain.GetComponent<CameraCollider> ();
	}
	
	// Update is called once per frame
	void Update () {
		float rotationX = Input.GetAxis ("Mouse X") * sensitivity;
		float rotationY = Input.GetAxis ("Mouse Y") * sensitivity;


		transform.RotateAround (targetOrbit.transform.position, Vector3.up, rotationX);

		if (cC.touch == true) {
			if (rotationY < 0) {
				transform.RotateAround (targetOrbit.transform.position, Vector3.left, rotationY);
			}
		} else {
			transform.RotateAround (targetOrbit.transform.position, Vector3.left, rotationY);
		}//s'assurer que la caméra ne passe pas au travers d'un mur


	}
}
