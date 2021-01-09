using UnityEngine;
using System.Collections;

[System.Serializable]			//serialize the boudary
[RequireComponent(typeof(GazePointDataComponent), typeof(Renderer))]		//get required component
public class Boundary			//boundary class that check the objects theis axises
{
	public float xMin, xMax, zMin, zMax;
}

public class CarController : MonoBehaviour {		//general game object controller
	
	public float speed;			//speed of the car
	public Boundary bd;			//boundary object
	public float flex;			//inclination pf the car
	
	private const float Scale = 1 / 50.0f;
	
	private EyeXHost _eyeXHost;			//eye tracker host component
	private GazePointDataComponent _gazePointDataComponent;		//eye tracker eye gaze point component
	private Renderer _rendererComponent;			//renderer component that attached the eye component
	private Vector2 gazePointOnDisplayPlaneMm;		//gaze point location 
	
	void Start()								// at the start detect the eye gaze positions
	{
		_eyeXHost = EyeXHost.GetInstance();
		_gazePointDataComponent = GetComponent<GazePointDataComponent>();
		_rendererComponent = GetComponent<Renderer>();
	}
	
	void FixedUpdate()		//called automatically by unity just before each physics steps
	{	
		Rigidbody Car = GetComponent<Rigidbody>(); 		// car game object rigidbody
		
		float actHorizontal = Input.GetAxis ("Horizontal");		//car game object X axis movement 
		float actVertical = Input.GetAxis ("Vertical");			//car game object  Y axis movement
		
		
		
		var displaySize = _eyeXHost.DisplaySize;			//display size (nm) state that application can use to retrive information if needed 
		var screenBounds = _eyeXHost.ScreenBounds;			//screen boundary (pixels) state that application can use to retrive information if needed
		var gazePoint = _gazePointDataComponent.LastGazePoint;
		
		var gazePointOnDisplayX = gazePoint.Display.x;			// x coordinate of eye gaze
		var gazePointOnDisplayY = gazePoint.Display.y;			//y coordinate of eye gaze 
		
		if (displaySize.IsValid &&								// the eye gaze points are in screen boundaries or not
		    screenBounds.IsValid && screenBounds.Value.Width > 0 && screenBounds.Value.Height > 0 &&
		    gazePoint.IsValid)
		{
			var normalizedGazePoint = new Vector2(				// normalization of the gaze points
				(float)((gazePointOnDisplayX - screenBounds.Value.X) / screenBounds.Value.Width),
				(float)((gazePointOnDisplayY - screenBounds.Value.Y) / screenBounds.Value.Height));
			
			gazePointOnDisplayPlaneMm = new Vector2(			//the last type of eye gazes data that displayed on the screen
				(float)((0.5 - normalizedGazePoint.x) * displaySize.Value.Width),
				(float)((0.5 - normalizedGazePoint.y) * displaySize.Value.Height));
			
			
			//			_rendererComponent.transform.position = new Vector3(
			//				gazePointOnDisplayPlaneMm.x * Scale,
			//				gazePointOnDisplayPlaneMm.y * Scale,
			//				0);
			
			//			_rendererComponent.enabled = true;
		}
		else
		{
			//			_rendererComponent.enabled = false;
		}
		
		
		Vector3 movement = new Vector3 (-gazePointOnDisplayPlaneMm.x * Scale, 0.0f, actVertical);		//in any movement of gaze points, it detects
		GetComponent<Rigidbody> ().velocity = movement;			// car object ridigbody velocity assign to movement  
		Car.velocity = movement * speed;						// giving a speed vector to car 
		
		Car.position = new Vector3 (Mathf.Clamp( Car.position.x,  bd.xMin, bd.xMax),  	//X coordinate of car position that can move limited boundaries
		                            0.0f, 												// Y coordinate  of the car that can not move to Y axis
		                            Mathf.Clamp(Car.position.x, bd.zMin, bd.zMax)		//Z coordinate of the car
		                            );
		
		Car.rotation = Quaternion.Euler (Car.velocity.x * flex, 90.0f, 0.0f);		// giving a car rotation to car game object
		
		
	}
}
