using UnityEngine;
using System.Collections;

public class DestroybyBoundary : MonoBehaviour {

	void OnTriggerExit(Collider other)		// if the game objects exit the boundary, destroy them
	{
		Destroy(other.gameObject);	


	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
