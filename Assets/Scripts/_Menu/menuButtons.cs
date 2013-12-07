using UnityEngine;
using System.Collections;

public class menuButtons : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	bool[] show = new bool[3];
	float buttonSpace = Screen.height / 6;
	int[] posBox = {120, 20};
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
				GUI.Label (new Rect (posBox[0] + 10, posBox[1] + 10, 100, 20), "Map!");
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
			if (show[2]){
				menuName = "Settings";
			}
			GUI.Box(new Rect(posBox[0],posBox[1],Screen.width - 140, Screen.height - 40), menuName);
		}

		if(GUI.Button(new Rect(20,Screen.height - buttonSpace*1 + 10,80,40), "Exit")) {
			Application.Quit();
		}
	}
}
