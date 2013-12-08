using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Controls	//It need to be a class becuse structs aren't serializable
{
	public KeyCode Up;
	public KeyCode Down;
	public KeyCode Left;
	public KeyCode Right;
	public KeyCode Jump;
	public KeyCode Attack;
}

public class PlayerInputController : MonoBehaviour 
{	
	public int m_player = 1;
	public List<Controls> m_playerControls = new List<Controls>();

	private PlayerMovement m_playerMovement;
	private Player m_playerController;
	void Start()
	{
		m_playerMovement = GetComponent<PlayerMovement>();
		m_playerController = GetComponent<Player>();
	}

	// Update is called once per frame
	void Update ()
	{
		float dir = 0;

		if(Input.GetKey(m_playerControls[m_player-1].Left))
			dir--;
		if(Input.GetKey(m_playerControls[m_player-1].Right))
			dir++;

		m_playerMovement.SetPlayerDir(new Vector2(dir,0));

		if(Input.GetKeyDown(m_playerControls[m_player-1].Jump))
		{
			m_playerMovement.TryToJump();
		}
		if(Input.GetKey(m_playerControls[m_player-1].Attack))
		{
			m_playerController.Fire();
		}

	}
}
