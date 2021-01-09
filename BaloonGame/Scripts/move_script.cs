using UnityEngine;
using System.Collections;

public class move_script : MonoBehaviour {

	public GameObject balloon1;
	public GameObject balloon2;
	public GameObject balloon3;

	Vector3 force;

	public Rigidbody rb; // defining rigibody to apply physics(force) to game objects

	private GazeAwareComponent _gazeAware; // to reach gaze data, defining gaze aware component 



	void Start(){
		_gazeAware = GetComponent<GazeAwareComponent>(); // initializing gaze aware component
	}


	void Awake()
	{	
//		force.x = Random.Range (0, 2);
//		force.y = Random.Range (0, 2);
//		force.z = Random.Range (-2, 2);
//		GetComponent<ConstantForce>().force = force;

		rb = GetComponent<Rigidbody>(); // initializing rigibody

//		rb.AddForce(Vector3.up);
//		rb.AddForce(Vector3.up);
//		rb.AddForce(Vector3.up);
//		rb.AddForce(Vector3.up);
//		rb.AddForce(Vector3.up);	
//		rb.AddForce (Vector3.left);
		//transform.position += Vector3.up * Time.time;


	
		force = new Vector3 (0, 18, 0); // create the initial force
		rb.AddForce(force); // applying initial force to the game objects
	}

	

		void Update(){
//		//rb.velocity = Vector3.zero;
//		ebe = Random.Range (0, 2);
//		print (ebe);
//		if (ebe == 0) {
//			force = new Vector3 (Random.Range (0, 30), 1, 0);
//		} 
//		else {
//			force = new Vector3 (Random.Range (-30, 0), 1, 0);
//		}
//
//
//		rb.AddForce(force);



//		if (i % 700 == 0) {
//			rb.velocity = Vector3.zero;
//			force = new Vector3 (0, 18, 0);
//			rb.AddForce(force);
//		}

		// create and apply random Vectors(forces) in the given range to give game objects oscillation movement
		force = new Vector3 (Mathf.Sin (Time.time * Random.Range (-5, 5)) * Random.Range (-5, 5), 0, 0);
		rb.AddForce (force);

		/* check is user looking at the game object or not for every frame and if he is looking for the specified time, 
		destroy that game object*/
		if (_gazeAware.HasGaze) {

			Destroy (balloon1);
			Destroy (balloon2);
			Destroy (balloon3);
		}
	}
}
