using UnityEngine;
using System.Collections;

public class GameOverTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	

	void OnTriggerEnter(Collider Other)
	{
		Application.LoadLevel(1); 
	}
	// Update is called once per frame
	void Update () 
	{
	
	}
}
