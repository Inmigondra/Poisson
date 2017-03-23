using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour {

	public int numberRessources; // ressources totale du niveau
	public int actualNumberRessources; // ressources restante du niveau
	public Transform startingPosition;
	public GameObject player;

	// Use this for initialization
	void Start () {
		StartGame ();
	}
	
	// Update is called once per frame
	void Update () {
		if (actualNumberRessources <= 0) {
			Debug.Log ("go to end level"); 
		}
	}

	public void GameOver (){
		Scene sceneToLoad = SceneManager.GetActiveScene ();
		SceneManager.LoadScene (sceneToLoad.name);
	}
	public void StartGame (){
		Instantiate (player, startingPosition.position, Quaternion.identity);
	}

}
