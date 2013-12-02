using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour 
{//8 0.6
	public float m_groundSpeed = 8;
	public float m_airSpeed = 5;
	public float m_jumpForce = 15;
	public float m_groundSlowdown = 0.6f;	//How %*100 of your current speed you slow down
	public float m_airSlowdown = 0.4f;	//How %*100 of your current speed you slow down

	private Vector2 m_currentDir;

	private Vector2 m_velocity = new Vector2(1,0);
	private bool m_grounded = false;
	

	void Update()
	{
		m_grounded = IsGrounded();

		Vector2 velocityToAdd = Vector2.zero;

		if(m_grounded)
		{
			velocityToAdd += m_currentDir * m_groundSpeed;
			velocityToAdd -= m_velocity * m_groundSlowdown;
		}
		else
		{
			velocityToAdd += m_currentDir * m_airSpeed;
			velocityToAdd -= m_velocity * m_airSlowdown;
		}

		if(!m_grounded)
		{
			rigidbody2D.velocity += new Vector2(0,Physics2D.gravity.y) * Time.deltaTime * rigidbody2D.gravityScale;
		}
		else if(rigidbody2D.velocity.y < 0)
		{
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
		}

		m_velocity += velocityToAdd;

		rigidbody2D.velocity = new Vector2(m_velocity.x, rigidbody2D.velocity.y);

		Vector2 collSize = GetComponent<BoxCollider2D>().size;

		RaycastHit2D hit 		= Physics2D.Raycast(new Vector2(transform.position.x,transform.position.y)  + Mathf.Sign(rigidbody2D.velocity.x) * new Vector2(collSize.x/2 + 0.01f, collSize.y/2) , new Vector2(Mathf.Sign(rigidbody2D.velocity.x),0), 0.01f);
		RaycastHit2D tempHit	= Physics2D.Raycast(new Vector2(transform.position.x,transform.position.y)  + Mathf.Sign(rigidbody2D.velocity.x) * new Vector2(collSize.x/2 + 0.01f, -collSize.y/2) , new Vector2(Mathf.Sign(rigidbody2D.velocity.x),0), 0.01f);
		RaycastHit2D tempHit2	= Physics2D.Raycast(new Vector2(transform.position.x,transform.position.y)  + Mathf.Sign(rigidbody2D.velocity.x) * new Vector2(collSize.x/2 + 0.01f, 0) , new Vector2(Mathf.Sign(rigidbody2D.velocity.x),0), 0.01f);

		if(!hit && tempHit)
		{
			hit = tempHit;
		}
		if(!hit && tempHit2)
		{
			hit = tempHit2;
		}

		if(hit)
		{
			float newPos = Mathf.Sign(rigidbody2D.velocity.x) * (collSize.x/2 + 0.01f);
			rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
			m_velocity = new Vector2(0, m_velocity.y);
			transform.position = new Vector2(hit.point.x - newPos, transform.position.y);

		}
	}

	public void SetPlayerDir(Vector2 dir)
	{
		m_currentDir = dir;
	}
	
	public void TryToJump()
	{		
		if(m_grounded)
		{
			rigidbody2D.AddForce(new Vector2(0, m_jumpForce));
		}
	}

	bool IsGrounded()
	{
		float jumpRayRange = 0.01f;

		Vector2 collSize = GetComponent<BoxCollider2D>().size;

		if(Physics2D.Raycast(new Vector2(transform.position.x,transform.position.y)  - new Vector2(0, collSize.y/2 + 0.01f) , -Vector2.up, jumpRayRange) ||
		   Physics2D.Raycast(new Vector2(transform.position.x,transform.position.y)  - new Vector2(collSize.x/2, collSize.y/2 + 0.01f) , -Vector2.up, jumpRayRange) ||
		   Physics2D.Raycast(new Vector2(transform.position.x,transform.position.y)  - new Vector2(-collSize.x/2, collSize.y/2 + 0.01f) , -Vector2.up, jumpRayRange))
			return true;

		return false;
	}

	void OnGUI()
	{
		GUILayout.Label("Real velocity: " 	+ rigidbody2D.velocity.ToString());
		GUILayout.Label("Code velocity: " 	+ m_velocity.ToString());
		GUILayout.Label("Grounded: " 		+ m_grounded.ToString());

	}
}