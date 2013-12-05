using UnityEngine;
using System;
using System.Collections.Generic;

public class guiConsole : MonoBehaviour
{
	public KeyCode toggleKey = KeyCode.BackQuote;
	
	List<ConsoleMessage> entries = new List<ConsoleMessage>();
	Vector2 scrollPos;
	bool show;
	
	const int margin = 20;
	
	public float width = 150;
	public float height = 180;
	
	GUIContent clearLabel    = new GUIContent("Clear",    "Clear console.");
	
	void OnEnable  () { Application.RegisterLogCallback(HandleLog); }
	void OnDisable () { Application.RegisterLogCallback(null); }

	struct ConsoleMessage
	{
		public readonly string  message;
		public readonly string  stackTrace;
		public readonly LogType	type;
		
		public ConsoleMessage (string message, string stackTrace, LogType type)
		{
			this.message    = message;
			this.stackTrace	= stackTrace;
			this.type       = type;
		}
	}
	
	void Update ()
	{ if (Input.GetKeyDown(toggleKey)) { show = !show; } }
	
	void OnGUI ()
	{
		Rect windowRect = new Rect(5,5,width,height);
		if (!show) { return; }
		
		windowRect = GUILayout.Window(123, windowRect, ConsoleWindow, "DebugConsole");
	}

	void ConsoleWindow (int windowID)
	{
		scrollPos = GUILayout.BeginScrollView(scrollPos);

		for (int i = 0; i < entries.Count; i++) {
			ConsoleMessage entry = entries[i];

			switch (entry.type) {
			case LogType.Error:
			case LogType.Exception:
				GUI.contentColor = Color.red;
				break;
				
			case LogType.Warning:
				GUI.contentColor = Color.yellow;
				break;
				
			default:
				GUI.contentColor = Color.white;
				break;
			}
			
			GUILayout.Label(entry.message);
		}
		
		GUI.contentColor = Color.white;
		GUILayout.EndScrollView();
		GUILayout.BeginHorizontal();

		if (GUILayout.Button(clearLabel)) { entries.Clear(); }

		GUILayout.EndHorizontal();
	}

	void HandleLog (string message, string stackTrace, LogType type)
	{
		ConsoleMessage entry = new ConsoleMessage(message, stackTrace, type);
		entries.Add(entry);
	}
}