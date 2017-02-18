using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalBehavior : MonoBehaviour {
	float timer;
	public float timerMax;
	public int type;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		timer += 1 * Time.deltaTime;
		if (timer >= timerMax)
			Destroy (gameObject); 
	}

	public int getTypeSignal(){
		return type;
	}
}
