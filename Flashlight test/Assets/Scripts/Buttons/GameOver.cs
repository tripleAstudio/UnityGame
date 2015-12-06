using UnityEngine;

// Скрипт главного меню
public class GameOver : MonoBehaviour
{
	private GUISkin skin;
	void Start()
	{
		skin = Resources.Load("buttons") as GUISkin;
	}
	
	void OnGUI()
	{
		Cursor.visible = true;
		{
			const int buttonWidth = 120;
			const int buttonHeight = 60;
		
			GUI.skin = skin;
		
			if (
				GUI.Button(
				new Rect(
				Screen.width / 2 - (buttonWidth / 2),
				(2 * Screen.height / 3) - (buttonHeight / 2),
				buttonWidth,
				buttonHeight
				),
				"Exit"
				)
				)
			{
				Application.Quit();
			}
		}
	}
}