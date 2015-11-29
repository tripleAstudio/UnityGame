using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class OpenUpDoorScriptExit : MonoBehaviour 
{


	public int open = 0;
	
	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
	}
	
	void OnTriggerEnter(Collider other) 
	{
		if (open == 1)
		{  
			open = 0;
			Open ();  
		} 
	}
	
	void Open()
	{
		Debug.Log("input open anim");
        GameObject doorExit = GameObject.FindWithTag("SF_Door_Exit_Level");
		doorExit.GetComponent<Animation>().Play("open");
	}
}
