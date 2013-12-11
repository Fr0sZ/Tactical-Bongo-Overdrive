using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DynamticCamera : MonoBehaviour {

	public float m_minSize = 10;
	public float m_maxSize = 25;

	public float m_sizeMultiplier = 0.15f;

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

		float size = m_minSize + dist * 0.15f;

		size = Mathf.Clamp(size, m_minSize, m_maxSize);

		GetComponent<Camera>().orthographicSize = size;
	}
}
