using UnityEngine;
using System.Collections;

public class menuButtons : MonoBehaviour {

	void Start () {
	
	}
	
	void Update () {

	}
	
	public string[] Scenes = new string[3] { "Grassland", "ConstructionSite", "PlayerTest" };
	bool[] show = new bool[3];
	float buttonSpace = Screen.height / 6;
	int[] posBox = {120, 20};
	
	// Settings
	public string[] settings = new string[3] { "Fastest", "Simple", "Fantastic" };
	public float fov = 10;
	public float fovMax = 10;
	public float fovMin = 0;
	public int currentShader = 0;
	
	void OnGUI ()
	{
		if(GUI.Button(new Rect(20,Screen.height - buttonSpace*4 + 10,80,40), "Play")) {
			show[0] = !show[0];
			show[1] = false;
			show[2] = false;
		}
		
		if(GUI.Button(new Rect(20,Screen.height - buttonSpace*3 + 10,80,40), "Multiplayer")) {
			show[1] = !show[1];
			show[0] = false;
			show[2] = false;
		}
		
		if(GUI.Button(new Rect(20,Screen.height - buttonSpace*2 + 10,80,40), "Settings")) {
			show[2] = !show[2];
			show[0] = false;
			show[1] = false;
		}

		if (show[0] || show[1] || show[2]){
			string menuName = null;
			// Map settings
			if (show[0] || show[1]){
				GUI.Label (new Rect (posBox[0] + 10, posBox[1] + 10, 100, 20), "Maps: " + Scenes[0] + ", "+ Scenes[1] + ", " + Scenes[2]);
				int count = 0;
				foreach (string s in Scenes)
				{
					if(GUI.Button(new Rect(posBox[0] + 10, posBox[1] + 50 + (count * 50), 150, 30), "Load " + s)) {
						Application.LoadLevel(s);
					}
					count += 1;
				}
			}
			
			// Singleplayer
			if (show[0]){
				menuName = "Play";
			}
			
			// Multiplayer 
			if (show[1]){
				menuName = "Multiplayer";
			}
			
			// Game Settings
			float heightPos = 10 + posBox[1];
			float settingSpace = 30;
			if (show[2]){
				// Quality Level
				GUI.Label (new Rect (posBox[0] + 10,heightPos,80,20), "QualityLevel");
				if(GUI.Button(new Rect(posBox[0] + 10,heightPos + settingSpace,80,20), "Terrible")) {
					QualitySettings.SetQualityLevel(0);
				}
				if(GUI.Button(new Rect(posBox[0] + 10 + 90,heightPos + settingSpace,80,20), "Standard")) {
					QualitySettings.SetQualityLevel(3);
				}
				if(GUI.Button(new Rect(posBox[0] + 10 + 180,heightPos + settingSpace,80,20), "ULTIMATE")) {
					QualitySettings.SetQualityLevel(6);
				}

				// FOV Slider
				GUI.Label (new Rect (posBox[0] + 10,heightPos + settingSpace * 2,80,20), "Horizontal FOV");
				fov = GUI.HorizontalSlider ( new Rect (posBox[0] + 40,heightPos + settingSpace * 3,190,20), fov, fovMin, fovMax );
				GUI.Label (new Rect (posBox[0] + 10,heightPos + settingSpace * 3,80,20), (fov*10).ToString());

				// Shader
				GUI.Label (new Rect (posBox[0] + 10,heightPos + settingSpace * 4,80,20), "Shader");
				if(GUI.Button(new Rect(posBox[0] + 10,heightPos + settingSpace * 5,80,20), "Normal")) {
					currentShader = 0;
				}
				if(GUI.Button(new Rect(posBox[0] + 10 + 90,heightPos + settingSpace * 5,80,20), "Cartoon")) {
					currentShader = 1;
				}
				if(GUI.Button(new Rect(posBox[0] + 10 + 180,heightPos + settingSpace * 5,80,20), "Old")) {
					currentShader = 2;
				}
			}
			GUI.Box(new Rect(posBox[0],posBox[1],Screen.width - (posBox[0] + posBox[1]), Screen.height - (2*posBox[1])), menuName);
		}

		if(GUI.Button(new Rect(20,Screen.height - buttonSpace*1 + 10,80,40), "Exit")) {
			Application.Quit();
		}
	}
}
