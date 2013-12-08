using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public GameObject m_owner;
	public int m_damage;

	void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.tag == "Player" && m_owner != collider.gameObject)
		{
			collider.gameObject.GetComponent<Player>().OnHit(m_owner, m_damage, transform.position, rigidbody2D.velocity);
			Destroy(gameObject);
		}else if(collider.tag == "Ground")
		{
		
			Destroy(gameObject);
		}
	}
}