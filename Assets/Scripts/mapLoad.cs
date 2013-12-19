using UnityEngine;
using System.Collections;

public class mapLoad : MonoBehaviour {

	public GameObject m_SpawnArea;

	void Start () {
		guiConsole.print("MP: " + PlayerPrefs.GetInt("multiplayer"));
		guiConsole.print("PL: " + PlayerPrefs.GetInt("players"));
		if (PlayerPrefs.GetInt("multiplayer") == 0)
		{
			// Singleplayer
			for (int a = 0; a < PlayerPrefs.GetInt("players"); a++)
			{
				float x = m_SpawnArea.transform.localScale.x;
				float xCord = m_SpawnArea.transform.position.x;
				float yCord = m_SpawnArea.transform.position.y;
				Instantiate(Resources.Load("Prefabs/AI"), new Vector2(Random.Range(-x/2,x/2)+xCord,yCord), Quaternion.identity);
			}
		}
		else
		{
			// Multiplayer
			for (int a = 0; a < PlayerPrefs.GetInt("players"); a++)
			{
				float x = m_SpawnArea.transform.localScale.x;
				float xCord = m_SpawnArea.transform.position.x;
				float yCord = m_SpawnArea.transform.position.y;
				Instantiate(Resources.Load("Prefabs/AI"), new Vector2(Random.Range(-x/2,x/2)+xCord,yCord), Quaternion.identity);
			}
		}
	}

	void Update () {
	
	}
}
