using UnityEngine;
using System.Collections;

public class Pistol_WBase : WBase {
	
	public GameObject Bullet;
	public float BulletForce = 10;
	public Vector3 BulletOffset = new Vector3 (0,0,0);
	public float Spread = 0.1f;
	public override void Fire(){
		if (CanFire())
		{
			PlayFireSound(); 
			int dir = 1;
			if(Mathf.RoundToInt(transform.parent.eulerAngles.y) == 180)
				dir = -1;

			Vector3 BulletOffsetTemp = BulletOffset;

			BulletOffsetTemp.x = BulletOffsetTemp.x * dir;
			Ammo--;
			GameObject newBullet = Instantiate(Bullet,BulletOffsetTemp + transform.position,Quaternion.Euler(transform.parent.eulerAngles)) as GameObject;
			newBullet.rigidbody2D.AddForce(new Vector2(BulletForce*dir,Random.Range(-Spread,Spread)));

		}
		
		
	}
	public override void OnPickUp(GameObject player)
	{

		if (player.GetComponent<Player>().Weapon == null){
		PlayPickUpSound();
		
			Destroy(GetComponent<Rigidbody2D>());

			GetComponent<BoxCollider2D>().enabled = false;
		
		transform.parent = player.transform;
		transform.localPosition = m_WeaponOffset;
		transform.localRotation = Quaternion.Euler(0,0,0);
		
		player.GetComponent<Player>().Weapon = this;
		Bullet.GetComponent<Bullet>().m_owner = player;
		}
	}
	
}
