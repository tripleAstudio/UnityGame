using UnityEngine;
using System.Collections;

public class PlayerBarDisplay : MonoBehaviour {
	public GUISkin mySkin;
	public PlayerStats Char;
	public bool Visible = true;
	
	void Start () {
	
	}
	
	void OnGUI () {
		if(Visible)
		{
			GUI.skin = mySkin;
			PlayerStats PlayerSt = (PlayerStats)Char.GetComponent("PlayerStats");
			float MaxHealth =PlayerSt.MaxHealth;
			float CurHealth = PlayerSt.CurHealth;
			float HealthBarLen = CurHealth/MaxHealth; 
		
	
			GUI.Box(new Rect(Screen.width - 255,Screen.height - 65,254,64), " ", GUI.skin.GetStyle("Bar"));
			
			GUI.Box(new Rect(Screen.width - 255,Screen.height - 65,254*HealthBarLen,64), " ", GUI.skin.GetStyle("HealthBar")); 
			
			GUI.Box(new Rect(Screen.width - 255,Screen.height - 65,254,64), " ", GUI.skin.GetStyle("PlayerBar"));
			
		}
	}
	

	void Update () 
	{
	
	}
}
