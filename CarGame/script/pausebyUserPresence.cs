using UnityEngine;
using System.Collections;

public class pausebyUserPresence : MonoBehaviour {

	public TextMesh pause_text;

	// Use this for initialization
	void Start () {
		//_userPresence = GetComponent<UserPresenceComponent>();
		//pause_text = GetComponent<TextMesh> ();
	}
	
	// Update is called once per frame
	void Update () {

		/* check is the user presence or not and if he is present continue the game, otherwise 
		pause the game until user is there*/
		if (GetComponent<UserPresenceComponent> ().IsUserPresent) {
			Time.timeScale = 1; //continue or start game
			pause_text.text = "";
		} 
		else {
			Time.timeScale = 0; // pause game
			pause_text.text = "Game Paused \n(User not Present)";
		}
	}
}
