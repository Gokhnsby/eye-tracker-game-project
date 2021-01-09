using UnityEngine;
using System.Collections;

public class CoinContact : MonoBehaviour {

public GameObject coin;		//coin game object
	public int score;		// the score that displayed at the game
	private GameController gameController;			// game controller 


	void Start(){		// when we start the game, check the game object that is created or not
		GameObject gameControllerobj = GameObject.FindWithTag ("GameController");		// retrieve the game object with using tag

		if (gameControllerobj != null) {		//if the game object is not null, get the component
			gameController = gameControllerobj.GetComponent <GameController>();
		}
		if (gameController == null)		// if the game object is not retrieved, say there iss no game object
			Debug.Log ("Game obje nirdeee!!!");

	}
	
	void OnTriggerEnter(Collider other) {		//Collider that chect the contacts of any objects

		if (other.tag == "Boundary")		// there is a cube boundary in the game, it check the game object is in the boundary or not
		{
			return;
		}
		if(other.tag == "Player")			// if the game object car contact with game object coin, destroy the coin game object and add score
			Instantiate (coin, transform.position, transform.rotation);
		gameController.AddScore (score);
		Destroy (gameObject);
	}
	

}
