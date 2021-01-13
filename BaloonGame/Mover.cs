using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour
{
	//public GameObject trc;
	public float speed;
	
	void Start ()
	{
		//Destroy (trc);

	}
	void Update (){

		GetComponent<Rigidbody>().velocity = -1 * transform.forward * speed;		//In the speed changes of trucks and coin, it triggered in the prefab of the objects
	}
	
}