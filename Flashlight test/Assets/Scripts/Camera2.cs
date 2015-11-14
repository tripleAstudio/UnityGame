using UnityEngine;
using System.Collections;

public class Camera2 : MonoBehaviour {
public Camera camera;
public Camera MainCamera;

void Update () {
     if (Input.GetKeyDown (KeyCode.C))
		if (MainCamera.enabled == false) {
			camera.enabled = false;	
			MainCamera.enabled = true;
		} else {
			MainCamera.enabled = false;	
			camera.enabled = true;
		}

		  	
}
}