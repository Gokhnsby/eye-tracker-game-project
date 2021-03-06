﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
[RequireComponent(typeof(GazePointDataComponent), typeof(Renderer))]
public class PlayerController : MonoBehaviour {

	public float speed;
	public Text countText;
	public Text winText;
	private Rigidbody rb;
	private int count;

	private const float Scale = 1 / 100.0f;
	
	private EyeXHost _eyeXHost;
	private GazePointDataComponent _gazePointDataComponent;
	private Renderer _rendererComponent;
	private Vector2 gazePointOnDisplayPlaneMm;

	void Start()
	{
		rb = GetComponent<Rigidbody> ();
		count = 0; //count for collecting number
		//countText.text = "Count: " + count.ToString ();
		SetCountText ();
		winText.text = "";

		_eyeXHost = EyeXHost.GetInstance();
		_gazePointDataComponent = GetComponent<GazePointDataComponent>();
		_rendererComponent = GetComponent<Renderer>();
	}
	// Use this for initialization
	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		//float moveVertical = Input.GetAxis ("Vertical");


		var displaySize = _eyeXHost.DisplaySize;
		var screenBounds = _eyeXHost.ScreenBounds;
		var gazePoint = _gazePointDataComponent.LastGazePoint;
		
		var gazePointOnDisplayX = gazePoint.Display.x;
		var gazePointOnDisplayY = gazePoint.Display.y;
		
		if (displaySize.IsValid &&
		    screenBounds.IsValid && screenBounds.Value.Width > 0 && screenBounds.Value.Height > 0 &&
		    gazePoint.IsValid)
		{
			var normalizedGazePoint = new Vector2(
				(float)((gazePointOnDisplayX - screenBounds.Value.X) / screenBounds.Value.Width),
				(float)((gazePointOnDisplayY - screenBounds.Value.Y) / screenBounds.Value.Height));
			
			gazePointOnDisplayPlaneMm = new Vector2(
				(float)((0.5 - normalizedGazePoint.x) * displaySize.Value.Width),
				(float)((0.5 - normalizedGazePoint.y) * displaySize.Value.Height));
	
		}


//		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, 0.0f);
//		rb.AddForce (movement * speed);
		Vector3 movement = new Vector3 (-gazePointOnDisplayPlaneMm.x * Scale, 0.0f, 0.0f);
		rb.AddForce (movement * speed);
	}
	void OnTriggerEnter(Collider other)
	{
		//Destroy (other.gameObject);
		if (other.gameObject.CompareTag ("PickUp")) {
			other.gameObject.SetActive(false);
			count = count + 1;
			//countText.text = "Count: " + count.ToString();
			SetCountText();
		}
		if (other.gameObject.CompareTag ("Harm1")) { //objects which are ended the game
			gameObject.SetActive (false);
			//count = count - 5;
			SetCountText (); 
			winText.text = "You failed";
		}
		if (other.gameObject.CompareTag ("Harm2")) {  // if harm2 tagged objects score will be reduced by 1
			gameObject.SetActive (true);  //sets objects visible
			other.gameObject.SetActive(true); //sets objects visible
			count = count - 1; // count variable reduced by one
			SetCountText ();  // calls SetCountText function
			//winText.text = "You failed";
		}
		if (other.gameObject.CompareTag ("PickUp_Bonus")) {  // if PickUp_Bonus tagged objects score will be increased by 2
			gameObject.SetActive (true);  //sets objects visible
			other.gameObject.SetActive(true); //sets objects visible
			count = count + 2; // count variable reduced by one
			SetCountText ();  // calls SetCountText function
			//winText.text = "You failed";
		}
		if (other.gameObject.CompareTag ("chuck_finish")) {  // if ball hit the chuch object game will be finnished
			gameObject.SetActive (false);  //sets the player ( ball ) object invisible
			other.gameObject.SetActive(true); //sets object visible

			SetCountText ();  // calls SetCountText function
			winText.text = "You have finnished the Game.";
		}
	}
	//Destroy(other.gameObject);
	//if(other.gameObject.CompareTag("Player"))
	//	gameObject.SetActive(false);
	void SetCountText()
	{
		countText.text = "Score: " + count.ToString();
		if (count >= 12) 
		{
			//winText.text = "You win";
		}
	}
}
