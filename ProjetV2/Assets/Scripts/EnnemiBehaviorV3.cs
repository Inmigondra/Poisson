using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnnemiBehaviorV3 : MonoBehaviour {
	public GameObject gameManager;
	GameManager gM;
	public GameObject player;
	AvatarBehavior aB;

	public enum States
	{
		playerUndetected, // Player non vu et pas dans la zone de repérage
		playerDetected, // Player non vu et dans la zone de détection
		hearRessource, // Lorsque le joueur récolte une entité
		playerSeen, // Player vu
		playerInRange // A bonne distance pour déclencher le pattern d'attaque
	}
	public States state; //Etat actuel de l'entité

	public float distancePlayer; // Distance hostile/avatar

	public float angleBase;
	public float angleVision;// champs de vision
	public float rangeDetected;//range de détection actuelle
	public float rangeMovement;//distance max entre 2 déplacements
	float timerHear;
	public float timerMaxHear;//temps max durant lequel, l'entité est attirée par l'écho de la ressource
	public bool asChecked; //dernière position du joueur vérifié
	public float diviseur; //pour limiter l'augmetation du champs de vision

	NavMeshAgent agent; // Référencie le component navmeshagent de l'entité
	Vector3 point; // Point dans l'espace, vérifiant si il est disponible
	Vector3 destination; // Point vers lequel l'entité se déplace
	Vector3 lastPositionPlayer; // Dernier point où le joueur a été vu
	Vector3 ressourceDetected; // Position de la dernière ressource detecté 
	bool searchingNewPoint; // L'entité cherche une nouvelle destination
	bool seePlayer; // L'avatar est dans le champs de vision
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

	// Use this for initialization
	void Start () {
		StartCoroutine (StartBehavior ());// Lance la coroutine permettant de lancer l'ia
		asChecked = true;
		searchingNewPoint = true;
		destination = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		distancePlayer = Vector3.Distance (player.transform.position, transform.position);//Connait en temps réel la distance etre l'entité et le joueur
		agent.SetDestination (destination);
		angleVision = angleBase + (aB.stamina / diviseur);
		switch (state) {
		case States.playerUndetected://--
			if (distancePlayer <= rangeDetected) {
				state = States.playerDetected;
			}
			if (searchingNewPoint == true) {
				if (randomPoint (transform.position, rangeMovement, out point )) {
					searchingNewPoint = false;
					destination = point;
					Debug.DrawRay (point, Vector3.up, Color.magenta, 10f);
				}
			}
			if (agent.remainingDistance <= 1f) {
				searchingNewPoint = true;
			}
			break;
		case States.hearRessource://--
			agent.SetDestination (ressourceDetected);
			timerHear += 1 * Time.deltaTime;
			if (timerHear >= timerMaxHear) { //c'est là que ça merde //transform.position == ressourceDetected
				searchingNewPoint = true;
				timerHear = 0;
				state = States.playerUndetected;
			}
			if (distancePlayer <= rangeDetected) { 
				state = States.playerDetected;
			}
			break;
		case States.playerDetected:
			transform.LookAt (player.transform.position);
			Vector3 direction = player.transform.position - transform.position;
			float angle = Vector3.Angle (direction, transform.forward);
			RaycastHit hit;
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
			if (agent.remainingDistance <= 1f) {
				state = States.playerDetected;
				SetResearch ();
			}
			break;
		}
	}


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
	/// <summary>
	/// Starts the behavior.
	/// </summary>
	/// <returns>The behavior.</returns>
	IEnumerator StartBehavior() {
		gameManager = GameObject.Find ("GameManager");
		gM = gameManager.GetComponent <GameManager> ();
		player = gM.spawnedPlayer;
		aB = player.GetComponent <AvatarBehavior>();
		agent = GetComponent <NavMeshAgent>();
		Debug.Log ("worked"); 
		yield break;
	}

}
