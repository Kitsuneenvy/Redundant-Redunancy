﻿using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.touchCount == 1){
			Touch touchZero = Input.GetTouch(0);
			Vector2 startPos = Vector2.zero;
			Vector2 direction = Vector2.zero;
			bool directionChosen = false;

			switch(touchZero.phase){
			case(TouchPhase.Began):
				startPos = touchZero.position;
				directionChosen = false;
				break;
			case(TouchPhase.Moved):
				direction = touchZero.position - startPos;
				break;
			case(TouchPhase.Ended):
				directionChosen = true;
				break;
			}


			if(directionChosen){
				Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;

				float touchDeltaMagnitude = (touchZero.position - touchZeroPrevPos).magnitude;

				Camera.main.transform.position = new Vector3(touchDeltaMagnitude*direction.x,touchDeltaMagnitude*direction.y,Camera.main.transform.position.z);
			}
		}

		// If there are two touches on the device...
		if (Input.touchCount == 2)
		{
			// Store both touches.
			Touch touchZero = Input.GetTouch(0);
			Touch touchOne = Input.GetTouch(1);
			
			// Find the position in the previous frame of each touch.
			Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
			Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
			
			// Find the magnitude of the vector (the distance) between the touches in each frame.
			float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
			float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;
			
			// Find the difference in the distances between each frame.
			float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;
			
			// If the camera is orthographic...
			if (camera.isOrthoGraphic)
			{
				// ... change the orthographic size based on the change in distance between the touches.
				camera.orthographicSize += deltaMagnitudeDiff * 1;
				
				// Make sure the orthographic size never drops below zero.
				camera.orthographicSize = Mathf.Max(camera.orthographicSize, 0.1f);
			}
			else
			{
				// Otherwise change the field of view based on the change in distance between the touches.
				camera.fieldOfView += deltaMagnitudeDiff * 1;
				
				// Clamp the field of view to make sure it's between 0 and 180.
				camera.fieldOfView = Mathf.Clamp(camera.fieldOfView, 0.1f, 179.9f);
			}
		}
	}
}