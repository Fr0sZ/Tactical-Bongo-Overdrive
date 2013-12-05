using UnityEngine;
using System.Collections;

public class guiHud : MonoBehaviour {



	// Ini Health
	void Start () {

	}

	// Update Health, current weapon + powerup
	void Update () {
		
	}

	public Texture2D icon;
	void OnGUI ()  {
		GUI.Box(new Rect (10,Screen.height - 60,100,50), icon);
	}
}
