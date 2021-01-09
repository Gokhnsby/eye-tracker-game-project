using UnityEngine;
using System.Collections;

public class Generator : MonoBehaviour {
	
	public GameObject balloon1;
	public GameObject balloon2;
	public GameObject balloon3;
	public int numberOfCubes; // it can be changed from the Unity gaming motor arbitrarily
	public int min, max; // it can be changed from the Unity gaming motor depends on the camera 
	

	// Use this for initialization
	void Start () {
		PlaceCubes();
	}

	// In this method three different kind of balloon is generated in random positions
	void PlaceCubes(){
		for (int i = 0; i < numberOfCubes; i++) {
			Instantiate(balloon1, GeneratedPosition1(),Quaternion.identity);
			Instantiate(balloon2, GeneratedPosition2(),Quaternion.identity);
			Instantiate(balloon3, GeneratedPosition3(),Quaternion.identity);
		}
	}

	//Random Vector3(position) is created with in the Range(min, max)
	Vector3 GeneratedPosition1()
	{
		int x,y,z;
		x = Random.Range(min,max);
		y = Random.Range(min,max);
		z = Random.Range(min,max);
		return new Vector3(x,y,z);
	}

	//Random Vector3(position) is created with in the Range(min, max)
	Vector3 GeneratedPosition2()
	{
		int x,y,z;
		x = Random.Range(min,max);
		y = Random.Range(min,max);
		z = Random.Range(min,max);
		return new Vector3(x,y,z);
	}

	//Random Vector3(position) is created with in the Range(min, max)
	Vector3 GeneratedPosition3()
	{
		int x,y,z;
		x = Random.Range(min,max);
		y = Random.Range(min,max);
		z = Random.Range(min,max);
		return new Vector3(x,y,z);
	}
	
	// Update is called once per frame
	void Update () {

	}
}