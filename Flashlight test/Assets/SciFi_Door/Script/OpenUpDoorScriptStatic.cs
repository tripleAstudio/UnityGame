using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class OpenUpDoorScriptStatic : MonoBehaviour 
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
		GameObject doorStatic = GameObject.FindWithTag("SF_Door_Static_Labirinte");
		doorStatic.GetComponent<Animation>().Play("open");
	}
}
