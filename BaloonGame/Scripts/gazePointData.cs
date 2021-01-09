using UnityEngine;

[RequireComponent(typeof(GazePointDataComponent), typeof(Renderer))]
public class gazePointData : MonoBehaviour
{
	// Scale: 1 mm maps to 0.001 units in world space
	private const float Scale = 1 / 10.0f;
	
	private EyeXHost _eyeXHost; // eye tracker host
	private GazePointDataComponent _gazePointDataComponent; // eye tracker component for the gaze point data
	private Renderer _rendererComponent;

	void Start()
	{
		_eyeXHost = EyeXHost.GetInstance();
		_gazePointDataComponent = GetComponent<GazePointDataComponent>();
		_rendererComponent = GetComponent<Renderer>();
	}
	
	
	void Update()
	{
		var displaySize = _eyeXHost.DisplaySize; // it returns the display size as coordinates (286.0 x 179.0)
		var screenBounds = _eyeXHost.ScreenBounds; // it returns the display size as pixels (2560 x 1600)
		var gazePoint = _gazePointDataComponent.LastGazePoint; // the last point user looks
		
		var gazePointOnDisplayX = gazePoint.Display.x; // it returns pixel in the x coordinate (1551.407)
		var gazePointOnDisplayY = gazePoint.Display.y; // // it returns pixel in the y coordinate (505.806)


		// controls the validation of datas
		if (displaySize.IsValid &&
		    screenBounds.IsValid && screenBounds.Value.Width > 0 && screenBounds.Value.Height > 0 &&
		    gazePoint.IsValid)
		{
			// pixel data should be normalized
			var normalizedGazePoint = new Vector2(
				(float)((gazePointOnDisplayX - screenBounds.Value.X) / screenBounds.Value.Width),
				(float)((gazePointOnDisplayY - screenBounds.Value.Y) / screenBounds.Value.Height));

			// normalized data should be integrated to coordinate system
			var gazePointOnDisplayPlaneMm = new Vector2(
				(float)((0.5 - normalizedGazePoint.x) * displaySize.Value.Width),
				(float)((0.5 - normalizedGazePoint.y) * displaySize.Value.Height));

			// move the object to the its new position in the world space
			_rendererComponent.transform.position = new Vector3(
				-gazePointOnDisplayPlaneMm.x * Scale,
				gazePointOnDisplayPlaneMm.y * Scale,
				0);
			
			_rendererComponent.enabled = true;
		}
		else
		{
			_rendererComponent.enabled = false;
		}
	}
}
