using UnityEngine;
using System.Collections;

public class RotateCoin : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	transform.Rotate (Vector3.forward) ;		// giving a rotation to coin in the game
	}
}
