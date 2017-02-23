using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RessourceBehavior : MonoBehaviour {
	public int type;
	public GameObject player;
	public GameObject parentComponent;
	public GameObject signalTrigger; // prefab de trigger, contient seulement un trigger représentant le signal
	public float distance;
	public Vector3 newDirection;
	float timer;
	public float timerToChange;
	delegate void ContactPlayer ();
	ContactPlayer cP;
	//private GameObject hostiles;
	// Use this for initialization
	void Start () {
		timer = timerToChange;
		parentComponent = transform.parent.gameObject;
		cP += CreateSignal;
		cP += DestroyRessource;
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		//Debug.Log (timer);
		if (timer <= 0) {
			CreateHostile ();
			DestroyRessource ();
		}
		/*distance = Vector3.Distance (transform.position, player.transform.position);
		if (distance <= 5f) {
			Vector3 dirRunaway = transform.position - player.transform.position;
			transform.Translate (new Vector3 (dirRunaway.normalized.x * 2f* Time.deltaTime, 0, dirRunaway.normalized.z * 5f * Time.deltaTime ));
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
		}*/
	}

	void NewDestination () {
		newDirection = new Vector3 (Random.value * Random.Range (-1,2), 0f, Random.value * Random.Range (-1,2));
	}
	void OnCollisionEnter (Collision col){
		if (col.gameObject.GetComponent<AvatarBehavior>()) {
			//Destroy (gameObject);
			cP ();
		}
	}


	void CreateHostile() {													//il faudra aussi assigner des couleurs a chaque type
		if (type == 0){
			GameObject ho = Instantiate (Resources.Load<GameObject> ("Ennemi"));
			ho.transform.position = transform.position;
		}
		else if (type == 1){
			GameObject ho = Instantiate (Resources.Load<GameObject> ("Cube"));
			ho.transform.position = transform.position;
		}


		//GameObject o = Instantiate (Resources.Load<GameObject> ("ChatBase"));
		/*GameObject ho = (GameObject)Instantiate (hostiles);
		hostiles.GetComponent<EnnemiBehaviorV2>().setTypeHostile (type);
		ho.transform.position = transform.position;*/
	}

	void CreateSignal () {													//ici pour donner son type a la ressource spawné
		GameObject signal = (GameObject)Instantiate (signalTrigger);
		signalTrigger.GetComponent<SignalBehavior> ().setTypeSignal (this.type);
		signal.transform.position = transform.position;
	}
	void DestroyRessource () {
		Destroy (gameObject);
	}

	public int getTypeRessource(){
		return this.type;
	}
}
