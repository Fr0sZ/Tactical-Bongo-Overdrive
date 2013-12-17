using UnityEngine;
using System.Collections;

public class HarryPotterFace_PBase : PBase {

	const float LightingBoltChance = 0.2f;
	const float LightingBoltSpeed = 30;
	private Vector3 HarryFaceOffset = new Vector3(0.42f,0.12f, 0);

	public GameObject m_harryPotterFace;
	public GameObject m_lightingBolt;

	public override void OnPickUp (GameObject player)
	{
		PlayPickUpSound();

		Destroy(GetComponent<Rigidbody2D>());
		Destroy(GetComponent<BoxCollider2D>());

		transform.parent = player.transform;
		transform.localPosition = HarryFaceOffset;
		transform.localRotation = Quaternion.Euler(0,0,0);

		player.GetComponent<Player>().TrackedPowerups.Add(this);
	}

	public override void OnHit(GameObject shooter, int dmg, Vector2 point, Vector2 dir)
	{

		if(LightingBoltChance > Random.value)
		{
			GameObject lighting = Instantiate(m_lightingBolt, transform.position, Quaternion.identity) as GameObject;
			lighting.GetComponent<Bullet>().m_owner = transform.parent.gameObject;
			lighting.rigidbody2D.AddForce(dir * -1 * LightingBoltSpeed);
		}
	}
}
