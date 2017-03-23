using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookAt : MonoBehaviour {
	public GameObject targetLook;// endroit regardé par la caméra
	// Update is called once per frame
	void Update () { 
		transform.LookAt (targetLook.transform.position);
	}
}
