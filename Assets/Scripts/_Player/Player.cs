using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private WBase m_weapon;

	void OnTriggerEnter2D(Collider2D collider)
	{
		PBase powerup;
		if(powerup = collider.GetComponent<PBase>())
		{
			powerup.OnPickUp(this.gameObject);
		}
	}

	public void OnHit()
	{
	}

	public void Attack()
	{
		if(m_weapon)
		{
			m_weapon.Fire();
		}
		else
		{

		}
	}
}
