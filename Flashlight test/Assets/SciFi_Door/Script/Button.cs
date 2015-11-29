/// <summary>
/// Button.
/// sgteam.ru
/// </summary>
using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {
	
	public Transform obj;
	private bool b_active = false;
	private bool guiShow = false;
	private bool checkDownButton = false;
	
	// Update is called once per frame
	void Update () 
	{
		float dist = Vector3.Distance(obj.position, transform.position);
		if(dist < 2 && !checkDownButton)
		{
			guiShow = true;
		}
		else if(dist > 2 || checkDownButton)
		{	
			guiShow = false;
		}
		if(Input.GetKeyDown(KeyCode.E) && dist < 2 && !b_active)
		{
			GameObject.Find ("base_Static").GetComponent<OpenUpDoorScriptStatic>().open = 1;
			GameObject.Find ("base_Generate").GetComponent<OpenUpDoorScriptGenerate>().open = 1;
			gameObject.transform.position += new Vector3(0.1f, 0, 0);
			b_active = true;
			checkDownButton = true;
		}
	}
	
	void OnGUI() 
	{ 
		if(guiShow) 
			GUI.Box(new Rect(Screen.width/2, Screen.height/2, 200, 25), "Press 'E' to open the doors"); 
	} 
}
