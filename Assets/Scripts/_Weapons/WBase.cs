using UnityEngine;
using System.Collections;

public abstract class WBase : MonoBehaviour {
	public float  FireCooldown;
	public int Ammo;
	public abstract void Fire();

	private float m_lastFire();
	protected virtual bool CanFire(){
		float timeSinceLastFire = Time.timeSinceLevelLoad - m_lastFire;

		if (Ammo>0 && timeSinceLastFire >= FireCooldown){
			m_lastFire = Time.timeSinceLevelLoad;
			return true;
		}
		return false;
	}
}
