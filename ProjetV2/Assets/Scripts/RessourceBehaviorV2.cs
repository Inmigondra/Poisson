using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RessourceBehaviorV2 : MonoBehaviour {
	public int type;//0,1 ou 2
	public GameObject[] signalTrigger = new GameObject[3];
	public GameObject[] hostiles = new GameObject[3];
	delegate void ContactPlayer ();
	ContactPlayer cP;
	delegate void HostileTransformation ();
	HostileTransformation hT;
	float timer;
	public float maxTimer; // temps avant transformation en hostile

	// Use this for initialization
	void Start () {
		cP += CreateSignal;
		cP += DestroyRessource;
		hT += CreateSignal;
		hT += CreateSignal;
	}
	
	// Update is called once per frame
	void Update () {
		timer += 1 * Time.deltaTime;
		if (timer >= maxTimer) {
			hT ();
		}
	}

	void OnCollisionEnter (Collision col){
		if (col.gameObject.tag == "Player") {
			cP ();
		}
	}
	void CreateSignal () {
		GameObject signal = (GameObject)Instantiate (signalTrigger[type]);
		signal.transform.position = transform.position;
	}
	void DestroyRessource () {
		Destroy (gameObject);
	}
	void CreateHostile () {
		GameObject ho = (GameObject)Instantiate (hostiles [type]);
		ho.transform.position = transform.position;
	}
}
