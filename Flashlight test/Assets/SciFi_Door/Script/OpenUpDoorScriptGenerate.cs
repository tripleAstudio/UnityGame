using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class OpenUpDoorScriptGenerate : MonoBehaviour 
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
		GameObject doorDynamic= GameObject.FindWithTag("SF_Door_Dynamic_Labirinte");
		doorDynamic.GetComponent<Animation>().Play("open");
	}
}
