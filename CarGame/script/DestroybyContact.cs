using UnityEngine;
using System.Collections;

public class DestroybyContact : MonoBehaviour {

	public GameObject crash;		//explotion of the contact - car game object & truck game object-
	public GameObject car_explode;
	private GameController gamecontroller_gameover; 	// game over controller in the game

	void Start(){
		GameObject gameObject_ = GameObject.FindWithTag ("GameController");		// retrieve the game object with using tag
		if (gameObject_ != null) {								//if the game object is not null, get the component
			gamecontroller_gameover = gameObject_.GetComponent<GameController>();
		}
		if (gamecontroller_gameover == null) {		// if the game object is not retrieved, say there iss no game object
			Debug.Log("GAme obje yok!!");
		}
	}

	void OnTriggerEnter(Collider other) //Collider that chect the contacts of any objects
	{
		if (other.tag == "Boundary")		// there is a cube boundary in the game, it check the game object is in the boundary or not
		{									
			return;
		}
	//Instantiate (crash, transform.position, transform.rotation);		

		if (other.tag == "Player"){						//the player car object contact with truck object.
			Instantiate(car_explode, other.transform.position, other.transform.rotation);		//activate the explosion
			gamecontroller_gameover.GameOver();			//activate th game over method
		}

		Destroy (other.gameObject);			//and destroy the both game objects
		Destroy (gameObject);
		 	
	}
}
