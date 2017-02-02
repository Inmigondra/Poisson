using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnnemiBehavior : MonoBehaviour {
	public GameObject player;
	AvatarBehavior aB;
	public float range;
	float distance;
	public float distanceDetected;
	public float angleVision;
	bool seePlayer;
	public bool searchingNewPoint; //cherche un point pour y aller
	Vector3 destination;
	NavMeshAgent agent;
	Vector3 point;
	public enum States
	{
		playerUndetected,
		playerDetected,
		playerSeen
	}
	public States state;
	//-------------------------------------------------------------------
	bool randomPoint(Vector3 center, float range, out Vector3 result ) {
		//for (int i = 0; i < 30; i++) {
			Vector3 randomPoint = center + Random.insideUnitSphere * range;
			NavMeshHit hit;
			if (NavMesh.SamplePosition (randomPoint, out hit, 1.0f, NavMesh.AllAreas)) {
				result = hit.position;
				return true;
			}
		//}
		result = Vector3.zero;
		return false;
	}
	//------------------------------------------------ bool permettant de check si une point est disponible
	void Start () {
		aB = player.GetComponent<AvatarBehavior> ();
		agent = GetComponent <NavMeshAgent> ();
		state = States.playerUndetected;
		searchingNewPoint = true;
	}
	
	// Update is called once per frame
	void Update() {
		distance = Vector3.Distance (transform.position, player.transform.position);
		if (distance <= distanceDetected && seePlayer == false) {
			state = States.playerDetected;
		} else if (seePlayer == false) {
			state = States.playerUndetected;
		}
		if (seePlayer == true) {
			state = States.playerSeen;
		}
		switch (state) {
		//Déplacement aléatoire, définir un comportement
		case States.playerUndetected: 
			if (searchingNewPoint == true) {
				if (randomPoint (transform.position, range, out point)) {
					searchingNewPoint = false;
					destination = point;
					Debug.DrawRay (point, Vector3.up, Color.blue, 50f);
				} 
			}
			if (agent.remainingDistance <= 1) {
				searchingNewPoint = true;
			}
			break;
		case States.playerDetected:
			Vector3 direction = player.transform.position - transform.position;
			float angle = Vector3.Angle (direction, transform.forward);
			RaycastHit hit;
			if (Physics.Raycast (transform.position, direction.normalized, out hit, distanceDetected)) {
				if (hit.collider.gameObject == player) {
					seePlayer = true;
					state = States.playerSeen;
				} else {
					transform.LookAt (player.transform.position);
					if (searchingNewPoint == true) {
						if (randomPoint (transform.position, range, out point)) {
							searchingNewPoint = false;
							destination = point;
							Debug.DrawRay (point, Vector3.up, Color.blue, 50f);
						} 
					}
					if (agent.remainingDistance <= 1) {
						searchingNewPoint = true;
					}
				}
			}
			break;
		case States.playerSeen:// complétement à refaire
			Vector3 lastPositionPlayer;
			direction = player.transform.position - transform.position;
			if (Physics.Raycast (transform.position, direction.normalized, out hit, distanceDetected)) {
				if (hit.collider.gameObject == player) {
					lastPositionPlayer = player.transform.position;
					destination = lastPositionPlayer;
				}
			}
			if (agent.remainingDistance <= 1) {
				state = States.playerUndetected;
			}
			break;
		}

		Debug.Log (point);
		Debug.Log (destination);
		agent.SetDestination (destination);
		
	}
	void SetPathRandom (){
		
	}
}
