using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DynamticCamera : MonoBehaviour {

	const float MinSize = 10;
	const float MaxSize = 25;

	private GameObject[] m_playerList;

	// Use this for initialization
	void Start () 
	{
		m_playerList = GameObject.FindGameObjectsWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 pos = m_playerList[0].transform.position - m_playerList[1].transform.position;

		transform.position = m_playerList[0].transform.position - pos / 2;
		transform.position = new Vector3(transform.position.x, transform.position.y, -10);

		float dist = Vector2.Distance(m_playerList[0].transform.position, m_playerList[1].transform.position);

		float size =   MinSize + dist * 0.1f;

		size = Mathf.Clamp(size, MinSize, MaxSize);

		GetComponent<Camera>().orthographicSize = size;
	}
}
