using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public GameObject m_owner;
	public int m_damage;

	public virtual void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.gameObject == m_owner)
			return;

		if(collider.tag == "Player" && m_owner != collider.gameObject)
		{
			collider.gameObject.GetComponent<Player>().OnHit(m_owner, m_damage, transform.position, rigidbody2D.velocity);

		}

		if(collider.rigidbody2D)
			collider.rigidbody2D.AddForce(rigidbody2D.velocity);

		Destroy(gameObject);
	}
}