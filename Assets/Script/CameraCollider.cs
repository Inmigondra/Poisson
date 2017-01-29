using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollider : MonoBehaviour {
	public bool touch; 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void OnTriggerEnter (){
		touch = true;
	}
	void OnTriggerExit (){
		touch = false;
	}

}
