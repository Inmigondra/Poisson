using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour {

	public int numberRessources; // ressources totale du niveau
	public int actualNumberRessources; // ressources récupéré
	public int requiredRessources; // ressources 
	public int nextLevel;
	public Transform startingPosition;
	public GameObject player;
	public GameObject spawnedPlayer;

	// Use this for initialization
	void Awake () {
		StartGame ();
	}
	
	// Update is called once per frame
	void Update () {
		if (actualNumberRessources <= 0) {
		}
	}

	public void GameOver (){
		Scene sceneToLoad = SceneManager.GetActiveScene ();
		SceneManager.LoadScene (sceneToLoad.name);
	}
	public void NextLevel (){
		if (actualNumberRessources >= requiredRessources) {
			SceneManager.LoadScene (nextLevel);
		} else {
			GameOver ();
		}
	}
	public void StartGame (){
		GameObject prefabPlayer = (GameObject)Instantiate (player);
		prefabPlayer.transform.position = startingPosition.position;
		spawnedPlayer = prefabPlayer;
	}

}
