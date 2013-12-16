using UnityEngine;
using System.Collections;

public class BlackHoleBullet : Bullet {

	const float AliveTime = 3;

	const float Radius = 10;
	const float Force = -1500;
	const float ExplodeRadius = 20;
	const float ExplodeForce = 250;
	const float ExplodeMaxDamage = 50;
	const float ExplodeMinDamage= 1;

	public GameObject m_explodeParticleEffect;

	private float m_timer = AliveTime;

	public override void OnTriggerEnter2D(Collider2D collider)
	{
	}

	void Update()
	{
		Collider2D[] closeObjs = Physics2D.OverlapCircleAll(transform.position, Radius);

		foreach(Collider2D obj in closeObjs)
		{
			if(obj.gameObject == gameObject)
			   continue;

			if(obj.rigidbody2D)
			{
				Vector3 direction = obj.transform.position - transform.position;
				obj.rigidbody2D.AddForceAtPosition(direction * Time.deltaTime * Force, transform.position);
			}
		}

		m_timer -= Time.deltaTime;

		if(m_timer <= 0)
		{
			OnDeath(closeObjs);
		}
	}

	void OnDeath(Collider2D[] closeObjs)
	{			
		foreach(Collider2D obj in closeObjs)
		{
			if(obj.gameObject == gameObject)
				continue;
			
			if(obj.rigidbody2D)
			{
				Vector3 direction = obj.transform.position - transform.position;
				obj.rigidbody2D.AddForceAtPosition(direction * ExplodeForce, transform.position);
			}
			
			if(obj.GetComponent<Player>() != null)
			{
				float dist = Vector2.Distance(transform.position, obj.transform.position);
				if(dist <= ExplodeRadius)
				{
					int damage =  Mathf.RoundToInt((1 - dist / ExplodeRadius) * (ExplodeMaxDamage - ExplodeMinDamage) + ExplodeMinDamage);
					obj.GetComponent<Player>().OnHit(m_owner, damage, obj.transform.position, (transform.position - obj.transform.position).normalized);
				}
			}
		}
		GameObject particle = Instantiate(m_explodeParticleEffect, transform.position, Quaternion.identity) as GameObject;
		Destroy(particle, 3);
		
		Destroy(gameObject);
	}
}
