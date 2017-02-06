using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RessourceBehavior : MonoBehaviour {
	public GameObject player;
	public GameObject parentComponent;
	public float distance;
	public Vector3 newDirection;
	float timer;
	public float maxTimer;
	// Use this for initialization
	void Start () {
		parentComponent = transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		distance = Vector3.Distance (transform.position, player.transform.position);
		if (distance <= 5f) {
			Vector3 dirRunaway = transform.position - player.transform.position;
			transform.Translate (new Vector3 (dirRunaway.normalized.x * 5f * Time.deltaTime, 0, dirRunaway.normalized.z * 5f * Time.deltaTime ));
		} else {
			transform.localPosition = new Vector3 (Mathf.Clamp (transform.localPosition.x, -10, 10), 0, Mathf.Clamp (transform.localPosition.z, -10, 10));
			transform.Translate (newDirection * Time.deltaTime * 3f);
			timer += 1*Time.deltaTime;
			if (timer <= maxTimer) {
				transform.Translate (newDirection * Time.deltaTime * 0.5f);
			} else {
				timer = 0f;
				NewDestination ();
			}
			//Vector3.ClampMagnitude ();
			//Invoke/*Repeating*/ ("NewDestination", 2f);
		}
	}

	void NewDestination () {
		newDirection = new Vector3 (Random.value * Random.Range (-1,2), 0f, Random.value * Random.Range (-1,2));
		print ("fack");
	}
	void OnCollisionEnter (Collision col){
		if (col.gameObject.tag == "Player") {
			Destroy (gameObject);
		}
	}
}
