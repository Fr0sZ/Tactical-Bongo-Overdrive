﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

	private int m_hp = 100;

	public GameObject m_bloodSpirit;
	public GameObject m_bloodParticleSystem;

	private WBase m_weapon;

	private List<PBase> m_trackedPowerups = new List<PBase>();

	public List<PBase> TrackedPowerups
	{
		get{return m_trackedPowerups;}
		set{m_trackedPowerups = value;}
	}

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

	void OnCollisionEnter2D(Collision2D collision)
	{
		PBase powerup;
		WBase weapon;
		if(powerup = collision.collider.GetComponent<PBase>())
		{
			powerup.OnPickUp(this.gameObject);

		}else if (weapon = collision.collider.GetComponent<WBase>())
		{
			weapon.OnPickUp(this.gameObject);
		}
	}
	
	public void OnHit(GameObject shooter, int dmg, Vector2 point, Vector2 dir)
	{
		Instantiate(m_bloodSpirit, point, Quaternion.identity);
		GameObject obj = Instantiate(m_bloodParticleSystem, point, Quaternion.identity) as GameObject;
		obj.transform.LookAt(point + dir);
		Destroy(obj, 3);
		Hp -= dmg;

		foreach(PBase powerup in m_trackedPowerups)
		{
			powerup.OnHit(shooter, dmg, point, dir);
		}
	}

	void OnDeath()
	{
		Debug.Log("Dead!");

		foreach(PBase powerup in m_trackedPowerups)
		{
			powerup.OnDeath();
		}

		gameObject.GetComponent<PlayerInputController>().enabled = false;
		gameObject.rigidbody2D.fixedAngle = false;

		foreach (Transform child in transform)
		{
			Destroy(child.gameObject);
		}
	}

	public void Fire()
	{
		if(m_weapon)
		{
			m_weapon.Fire();
			foreach(PBase powerup in m_trackedPowerups)
			{
				powerup.OnFire();
			}
		}
		else
		{

		}
	}
}
