using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnnemiBehavior : MonoBehaviour {
	public GameObject player;
	public float range;
	Vector3 destination;
	NavMeshAgent agent;
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
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update() {
		Vector3 point;
		if (randomPoint(transform.position, range, out point)) {
			Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
		}
	}
}
