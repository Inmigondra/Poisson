using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnnemiBehaviorV2 : MonoBehaviour {

	public enum States
	{
		playerUndetected,
		playerDetected,
		playerSeen
	}
	public States state;
	public GameObject player; //référencie l'avatar
	AvatarBehavior aB;// référencie le script AvatarBehavior

	public float rangeDetected; //distance à laquelle l'entité detecte le joueur 
	NavMeshAgent agent; //référencie le component navmeshagent de l'entité

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
