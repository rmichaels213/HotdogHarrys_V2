/*----------------------------------------
Filename:		ControllerCamera.cs	
Created By:		Ryan Michaels
Created Date:	04/22/2018
Updated Date:	04/22/2018
----------------------------------------*/

using UnityEngine;

public class ControllerCamera : MonoBehaviour
{
	public GameObject target;
	private Vector3 offset;

	public float cameraAcceleration = 5f;
	public float cameraDeceleration = 10f;
	public float maxCameraHeight = 10f;
	public float minCameraHeight = 3f;

	public bool isAccelerating;
	public bool isDecelerating;

	/// <summary>
	/// Called once on initialization.
	/// </summary>
	void Start()
	{
		offset = transform.position - target.transform.position;
	}

	/// <summary>
	/// Called every frame.
	/// </summary>
	void LateUpdate()
	{
		// Follow truck
		transform.position = target.transform.position + offset;

		// Handle zoom of camera
		if ( ControllerTruck.truck.isAccelerating || ControllerTruck.truck.isDecelerating )
		{
			if ( GetComponent<Camera>().orthographicSize < maxCameraHeight )
			{
				GetComponent<Camera>().orthographicSize += cameraAcceleration * Time.deltaTime;
			}
		}
		else
		{
			if ( GetComponent<Camera>().orthographicSize > minCameraHeight )
			{
				GetComponent<Camera>().orthographicSize -= cameraDeceleration * Time.deltaTime;
			}
		}

		// Handle clamps for camera
		if ( GetComponent<Camera>().orthographicSize > maxCameraHeight )
		{
			GetComponent<Camera>().orthographicSize = maxCameraHeight;
		}
		if ( GetComponent<Camera>().orthographicSize < minCameraHeight )
		{
			GetComponent<Camera>().orthographicSize = minCameraHeight;
		}

	}
}
