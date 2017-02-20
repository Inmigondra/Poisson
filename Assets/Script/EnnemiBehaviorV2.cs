using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnnemiBehaviorV2 : MonoBehaviour {

	public int type;

	public enum States
	{
		playerUndetected, // Player non vu et pas dans la zone de repérage
		playerDetected, // Player non vu et dans la zone de détection
		hearRessource, // Lorsque le joueur récolte une entité
		playerSeen // Player vu
	}
	public States state; // Donne l'état de recherche de l'entité

	public GameObject player; // Référencie l'avatar
	AvatarBehavior aB;// Référencie le script AvatarBehavior
	public float distancePlayer; // Distance entre Entité/Avatar

	public float angleVision; // Angle de vision de l'entité
	public float baseRangeDetection;
	public float rangeDetected; // Distance à laquelle l'entité detecte le joueur 
	public float rangeMovement; // Distance max entre 2 déplacements
	float timerHear;
	public float timerHearMax;// temps maximum pouvant etre entendu
	NavMeshAgent agent; // Référencie le component navmeshagent de l'entité
	Vector3 point; // Point dans l'espace, vérifiant si il est disponible
	Vector3 destination; // Point vers lequel l'entité se déplace
	Vector3 lastPositionPlayer; // Dernier point où le joueur a été vu
	Vector3 ressourceDetected; // Position de la dernière ressource detecté 
	bool searchingNewPoint; // L'entité cherche une nouvelle destination
	bool seePlayer; // L'avatar est dans le champs de vision
	public SignalBehavior sigBeh;
	/// <summary>
	/// Randoms the point.
	/// Permet de choisir un point aléatoire autour de l'entité et retourné si elle existe ou non
	bool randomPoint (Vector3 center, float range, out Vector3 result) {
		Vector3 randomPoint = center + Random.insideUnitSphere * range;
		NavMeshHit hit;
		if (NavMesh.SamplePosition (randomPoint, out hit, 1.0f, NavMesh.AllAreas)) {
			result = hit.position;
			return true;
		}
		result = Vector3.zero;
		return false;
	}

	/// <summary>
	/// Start this instance.
	/// </summary>

	void Awake () {
		player = GameObject.Find ("player-3rd (1)");
	}

	void Start () {
		
		aB = player.GetComponent<AvatarBehavior> ();
		agent = GetComponent <NavMeshAgent> ();
		searchingNewPoint = true;
	}
	
	// Update is called once per frame
	void Update () {
		distancePlayer = Vector3.Distance (player.transform.position, transform.position);// Connait en temps réel la distance entre l'entité et le joueur
		//Debug.Log (distancePlayer); 
		agent.SetDestination (destination);
		if (this.type == 0){
			rangeDetected = baseRangeDetection + (aB.stamina0);
		}
		if (this.type == 1){
			rangeDetected = baseRangeDetection + (aB.stamina1);
		}
		//rangeDetected = baseRangeDetection + (aB.stamina); // Modifie la range de détection en fonction de la stamina //test types !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
		switch (state) {
		case States.playerUndetected:
			if (distancePlayer <= rangeDetected /*&& seePlayer == false*/) { // Joueur à distance de détection
				state = States.playerDetected;
			}
			if (searchingNewPoint == true) {
				if (randomPoint (transform.position, rangeMovement, out point)) {
					searchingNewPoint = false;
					destination = point;
					Debug.DrawRay (point, Vector3.up, Color.blue, 50f);
				} 
			}
			if (agent.remainingDistance <= 1f){
				searchingNewPoint = true;
			}
			break;
		case States.hearRessource:
			//transform.position = Vector3.MoveTowards (transform.position, ressourceDetected, 0.01f);
			agent.SetDestination (ressourceDetected);
			timerHear += Time.deltaTime;
			Debug.Log (timerHear);
			if (timerHear >= timerHearMax) { //c'est là que ça merde //transform.position == ressourceDetected
				searchingNewPoint = true;
				timerHear = 0;
				state = States.playerUndetected;
			}
			if (distancePlayer <= rangeDetected /*&& seePlayer == false*/) { // Joueur à distance de détection
				state = States.playerDetected;
			}
			break;
		case States.playerDetected:
			Vector3 direction = player.transform.position - transform.position;
			float angle = Vector3.Angle (direction, transform.forward);
			RaycastHit hit; 
			transform.LookAt (player.transform.position);
			if (angle <= angleVision) {
				if (Physics.Raycast (transform.position, direction.normalized, out hit, rangeDetected)) {
					if (hit.collider.gameObject == player) {
						seePlayer = true;
						state = States.playerSeen;
					} else {
						if (searchingNewPoint == true) {
							if (randomPoint (transform.position, rangeMovement, out point)) {
								searchingNewPoint = false;
								destination = point;
								Debug.DrawRay (point, Vector3.up, Color.blue, 50f);
							} 
						}
						if (agent.remainingDistance <= 1f) {
							searchingNewPoint = true;
						}
					}
				}
			}
			if (distancePlayer >= rangeDetected) {
				state = States.playerUndetected;
				SetResearch ();
			}
			break;
		case States.playerSeen:
			direction = player.transform.position - transform.position;
			if (Physics.Raycast (transform.position, direction.normalized, out hit, rangeDetected)) {
				if (hit.collider.gameObject == player) {
					lastPositionPlayer = player.transform.position;
					destination = lastPositionPlayer;
				}
			}
			if (agent.remainingDistance <= 1) {
				state = States.playerDetected;
				SetResearch ();
			}
			break;
		}
	}

	/// <summary>
	/// Sets the iddle. remet les variables de bases à zero
	void SetResearch () {
		searchingNewPoint = true;
	}


	/// <summary>
	/// Raises the draw gizmos event.
	/// </summary>
    void OnDrawGizmos() {
		DrawGizmosCone (transform.position, transform.forward, transform.up, angleVision, rangeDetected); //Champs de vision
    }
	void DrawGizmosCone(Vector3 pos, Vector3 dir, Vector3 up, float angle, float distance) {
		Vector3 leftVector = Quaternion.AngleAxis(-angle, up) * dir * distance;
		Gizmos.color = Color.red;
		Gizmos.DrawLine(pos, pos + leftVector);
		Vector3 rightVector = Quaternion.AngleAxis(angle, up) * dir * distance;
		Gizmos.color = Color.red;
		Gizmos.DrawLine(pos, pos + rightVector);
		Vector3 lastpt = pos + rightVector;
		int numsegments = 20;
		float angleStep = angle * 2.0f / numsegments;
		for (int i = 0 ; i<= numsegments; i++){
			Vector3 newPt = pos +Quaternion.AngleAxis(- angleStep * i , up) * rightVector;
			Gizmos.color = Color.red;
			Gizmos.DrawLine(lastpt, newPt);
			lastpt = newPt;
		}
	}

	void OnTriggerEnter (Collider col){
		if (col.GetComponent<SignalBehavior>()) {
			if (type == col.GetComponent<SignalBehavior>().getTypeSignal ()) { //SignalBehavior.getTypeSignal(signal)){
				state = States.hearRessource;
				ressourceDetected = col.gameObject.transform.position;
			}
		}
	}
}
