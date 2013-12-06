using UnityEngine;
using System.Collections;

public class guiPauseMenu : MonoBehaviour {

	public KeyCode toggleKey = KeyCode.Escape;
	public float width = 110;
	public float height = 160;
	public float heightPos = 190;
	public float buttonSpace = 30;
	public float widthSettings = 220;
	float widthSettingsC = 0;
	bool show;
	bool showSettings;
	
	void Update ()
	{ 
		if (Input.GetKeyDown(toggleKey)) { 
			showSettings = false;
			show = !show; 
		} 

		if (showSettings)
			widthSettingsC = widthSettings;
		else
			widthSettingsC = 0;
	}

	void OnGUI ()
	{
		if (!show) { return; }
		GUI.Box(new Rect(5, 190, width + widthSettingsC, height), "" );

		if(GUI.Button(new Rect(20,heightPos + 10,80,20), "Settings")) {
			showSettings = !showSettings;
			if (showSettings){

			}
		}

		if(GUI.Button(new Rect(20,heightPos + buttonSpace*1 + 10,80,20), "Restart")) {
			Application.LoadLevel(Application.loadedLevel);
		}

		if(GUI.Button(new Rect(20,heightPos +  buttonSpace*2 + 10,80,20), "MainMenu")) {
			Application.LoadLevel(1);
		}

		if(GUI.Button(new Rect(20,heightPos + buttonSpace*3 + 10,80,20), "Quit")) {
			Application.Quit();
		}
	}
}
