using UnityEngine;
using System.Collections;

public class guiPauseMenu : MonoBehaviour {

	public KeyCode toggleKey = KeyCode.Escape;
	public float width = 110;
	public float height = 130;
	public float heightPos = 190;
	public float buttonSpace = 30;
	bool show;

	// Ini
	void Start () {
	
	}

	void Update ()
	{ 
		if (Input.GetKeyDown(toggleKey)) { 
			show = !show; 
			guiConsole.print("Menu = " + show);
		} 
	}

	void OnGUI ()
	{
		if (!show) { return; }
		GUI.Box(new Rect(5, 190, width, height), "Menu");

		if(GUI.Button(new Rect(20,heightPos + buttonSpace,80,20), "Restart")) {
			Application.LoadLevel(Application.loadedLevel);
		}

		if(GUI.Button(new Rect(20,heightPos +  buttonSpace*2,80,20), "MainMenu")) {
			Application.LoadLevel(1);
		}

		if(GUI.Button(new Rect(20,heightPos + buttonSpace*3,80,20), "Quit")) {
			Application.Quit();
		}
	}
}
