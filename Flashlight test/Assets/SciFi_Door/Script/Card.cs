/// <summary>
/// Button.
/// sgteam.ru
/// </summary>
using UnityEngine;
using System.Collections;

public class Card : MonoBehaviour {
	
	public Transform obj;
	private bool guiShow = false;
	
	// Update is called once per frame
	void Update () 
	{
		float dist = Vector3.Distance(obj.position, transform.position);
		if(dist < 2)
		{
			guiShow = true;
		}
		else if(dist > 2)
		{	
			guiShow = false;
		}
		if(Input.GetKeyDown(KeyCode.E) && dist < 2)
		{
			GameObject.Find ("base_Exit").GetComponent<OpenUpDoorScriptExit>().open = 1;
			Destroy(gameObject);
		}
	}
	
	void OnGUI() 
	{ 
		if(guiShow) 
			GUI.Box(new Rect(Screen.width/2, Screen.height/2, 200, 25), "Press 'E', that to take"); 
	} 
}
