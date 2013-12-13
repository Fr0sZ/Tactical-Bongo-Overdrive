﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {

	public List<GameObject> m_ObjectsToSpawn;
	public GameObject m_SpawnArea;

	private float m_nextSpawn;
	private float m_lastTime;
	// Use this for initialization
	void Start () {
		m_lastTime = Time.timeSinceLevelLoad;
		m_nextSpawn = Random.Range(2,20);
	}
	
	// Update is called once per frame
	void Update () {

		if ((Time.timeSinceLevelLoad-m_lastTime)> m_nextSpawn){
			m_lastTime = Time.timeSinceLevelLoad;
			float x = m_SpawnArea.transform.localScale.x;
			float y = m_SpawnArea.transform.localScale.y;
			float xCord = m_SpawnArea.transform.position.x;
			float yCord = m_SpawnArea.transform.position.y;
			GameObject newObject = Instantiate(m_ObjectsToSpawn[Random.Range(0,m_ObjectsToSpawn.Count)], new Vector2(Random.Range(-x/2,x/2)+xCord,yCord), Quaternion.identity) as GameObject;
			m_nextSpawn = Random.Range(2,20);
			Debug.Log (m_nextSpawn);
		}
	}
}