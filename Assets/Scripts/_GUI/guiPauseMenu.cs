using UnityEngine;
using System.Collections;

public class guiPauseMenu : MonoBehaviour {

	public KeyCode toggleKey = KeyCode.Escape;
	public float width = 110;
	public float height = 160;
	public float heightPos = 190;
	public float buttonSpace = 30;
	public float widthSettings = 280;
	float widthSettingsC = 0;
	bool show;
	bool showSettings;
	public string MenuSceneName = "menu";
	public string[] settings = new string[3] { "Fastest", "Simple", "Fantastic" };
	public Camera MainCamera;
	public float fov = 10;
	public float fovMax = 10;
	public float fovMin = 0;
	
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

		// Update FOV
		Rect CameraFov = new Rect(0,0,fov/10,1);
		if (CameraFov != MainCamera.rect)
		{
			MainCamera.rect = CameraFov;
			Debug.Log("Fov: " + fov.ToString());
		}
	}

	void OnGUI ()
	{
		if (!show) { return; }
		GUI.Box(new Rect(5, 190, width + widthSettingsC, height), "" );

		// Settings
		if(GUI.Button(new Rect(20,heightPos + 10,80,20), "Settings")) {
			showSettings = !showSettings;
		}
		if (showSettings){
			// Quality Level
			GUI.Label (new Rect (120,heightPos + 10,80,20), "QualityLevel");
			if(GUI.Button(new Rect(120,heightPos + 10 + buttonSpace,80,20), "Terrible")) {
				QualitySettings.SetQualityLevel(0);
			}
			if(GUI.Button(new Rect(210,heightPos + 10 + buttonSpace,80,20), "Standard")) {
				QualitySettings.SetQualityLevel(3);
			}
			if(GUI.Button(new Rect(300,heightPos + 10 + buttonSpace,80,20), "ULTIMATE")) {
				QualitySettings.SetQualityLevel(6);
			}

			// FOV Slider
			GUI.Label (new Rect (120,heightPos + 10 + buttonSpace*2,80,20), "Horizontal FOV");
			fov = GUI.HorizontalSlider ( new Rect (180,heightPos + 10 + buttonSpace*3,190,20), fov, fovMin, fovMax );
			GUI.Label (new Rect (120,heightPos + 10 + buttonSpace*3,80,20), fov.ToString());

		}

		// Restart
		if(GUI.Button(new Rect(20,heightPos + buttonSpace*1 + 10,80,20), "Restart")) {
			Application.LoadLevel(Application.loadedLevel);
		}

		// Main Menu
		if(GUI.Button(new Rect(20,heightPos +  buttonSpace*2 + 10,80,20), "MainMenu")) {
			Application.LoadLevel("menu");
		}

		// Quit
		if(GUI.Button(new Rect(20,heightPos + buttonSpace*3 + 10,80,20), "Quit")) {
			Application.Quit();
		}
	}
}
