using UnityEngine;
using System.Collections;

public class ChangeSpeed : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}
	public void Changespd (){
		GetComponent<Rigidbody>().velocity = -1 * (transform.forward * 2);		//change the game speed of the game object which has rigidbody
		//mover method is used for speed changes. 
	
	}

}
