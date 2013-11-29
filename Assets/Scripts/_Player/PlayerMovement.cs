using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour 
{
	public float m_movementSpeed = 5;
	public float m_jumpForce = 15;
	public float m_speedSlowdown = 0.1f;
	
	private Vector2 m_currentDir;

	private Vector2 m_velocity = new Vector2(1,0);
	private bool m_grounded = false;


	void Start()
	{

	}

	void Update()
	{
		//m_currentDir = new Vector2(Input.GetAxis("Horizontal"),0);

		float jumpRayRange = 0.01f;

		m_grounded = Physics2D.Raycast(new Vector2(transform.position.x,transform.position.y)  - new Vector2(0, GetComponent<BoxCollider2D>().size.y/2 + 0.01f) , -Vector2.up, jumpRayRange);

		Vector2 velocityToAdd = Vector2.zero;

		velocityToAdd += m_currentDir * m_movementSpeed;

		velocityToAdd -= m_velocity * m_speedSlowdown;


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

	void OnGUI()
	{
		GUILayout.Label("Real velocity: " 	+ rigidbody2D.velocity.ToString());
		GUILayout.Label("Code velocity: " 	+ m_velocity.ToString());
		GUILayout.Label("Grounded: " 		+ m_grounded.ToString());

	}
}