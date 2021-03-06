﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

	private int m_hp = 100;

	public GameObject m_bloodSpirit;
	public GameObject m_bloodParticleSystem;
	public GameObject m_deadBody;
	public GameObject m_SpawnArea;
	public GameObject m_damageNumber;
	
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

		foreach(PBase powerup in m_trackedPowerups)
		{
			powerup.OnHit(shooter, dmg, point, dir);
		}

		GameObject dmgNumber = Instantiate(m_damageNumber, (Vector2)transform.position + new Vector2(0,1.5f), Quaternion.identity) as GameObject;
		dmgNumber.GetComponent<TextMesh>().text = dmg.ToString();

		Hp -= dmg;
	}

	void OnDeath()
	{
		foreach(PBase powerup in m_trackedPowerups)
		{
			powerup.OnDeath();
		}

		GameObject deadBody = Instantiate(m_deadBody, transform.position, Quaternion.identity) as GameObject;
		deadBody.GetComponent<SpriteRenderer>().color = GetComponent<SpriteRenderer>().color - new Color(0.15f,0.15f,0.15f, 0);

		float x = m_SpawnArea.transform.localScale.x;
		float xCord = m_SpawnArea.transform.position.x;
		float yCord = m_SpawnArea.transform.position.y;

		GameObject newPlayer;

		if(GetComponent<AI>())
			newPlayer = Instantiate(Resources.Load("Prefabs/AI"), new Vector2(Random.Range(-x/2,x/2)+xCord,yCord), Quaternion.identity) as GameObject;
		else
		{
			newPlayer = Instantiate(Resources.Load("Prefabs/Player"), new Vector2(Random.Range(-x/2,x/2)+xCord,yCord), Quaternion.identity) as GameObject;
			newPlayer.GetComponent<PlayerInputController>().m_player = GetComponent<PlayerInputController>().m_player;
		}

		newPlayer.GetComponent<Player>().m_SpawnArea = m_SpawnArea;
		
		Destroy(gameObject);

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

	public void Throw()
	{

		if(m_weapon)
		{
			int dir = 1;
			if(Mathf.RoundToInt(transform.eulerAngles.y) == 180)
				dir = -1;

			m_weapon.transform.parent = null;
			m_weapon.gameObject.AddComponent<Rigidbody2D>();
			m_weapon.GetComponent<Rigidbody2D>().fixedAngle = true;
			m_weapon.gameObject.transform.position = new Vector3(2*dir,0,0)+m_weapon.gameObject.transform.position;
			m_weapon.GetComponent<BoxCollider2D>().enabled = true;
			m_weapon.GetComponent<Rigidbody2D>().AddForce (new Vector3(1000*dir,0,0));
			m_weapon = null;

		}

	}
}
