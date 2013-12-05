using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private int m_hp = 100;

	public GameObject m_bloodSpirit;
	public GameObject m_bloodParticleSystem;

	private WBase m_weapon;

	public int Hp
	{
		get{return m_hp;}
		set
		{
			m_hp = value;

			if(m_hp <= 0)
			{
				OnDeath();
			}
		}
	}

	public WBase Weapon
	{
		get{return m_weapon;}
		set{m_weapon = value;}
	}


	void OnTriggerEnter2D(Collider2D collider)
	{
		PBase powerup;
		if(powerup = collider.GetComponent<PBase>())
		{
			powerup.OnPickUp(this.gameObject);
		}
	}

	public void OnHit(int dmg, Vector2 point, Vector2 dir)
	{
		Instantiate(m_bloodSpirit, point, Quaternion.identity);
		GameObject obj = Instantiate(m_bloodParticleSystem, point, Quaternion.identity) as GameObject;
		obj.transform.LookAt(point + dir);
	}

	void OnDeath()
	{
	}

	public void Fire()
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
