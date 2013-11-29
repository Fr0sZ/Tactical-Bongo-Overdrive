using UnityEngine;
using System.Collections;

public class PlayerInputController : MonoBehaviour 
{	
	private PlayerMovement m_playerMovement;

	void Start()
	{
		m_playerMovement = GetComponent<PlayerMovement>();
	}

	// Update is called once per frame
	void Update () 
	{
		m_playerMovement.SetPlayerDir(new Vector2(Input.GetAxis("Horizontal"),0));

		if(Input.GetButtonDown("Jump"))
		{
			m_playerMovement.TryToJump();
		}
	}
}
