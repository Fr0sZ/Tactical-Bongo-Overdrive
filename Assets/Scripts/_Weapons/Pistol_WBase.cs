using UnityEngine;
using System.Collections;

public class Pistol_WBase : WBase {
	
	public GameObject Bullet;
	public override void Fire(){
		if (CanFire())
		{
			GameObject newBullet = Instantiate(Bullet,transform.position,Quaternion.identity) as GameObject;
			newBullet.rigidbody2D.AddForce(new Vector2(1000,0));
		}
		
		
	}
	
}
