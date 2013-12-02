using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	void OnTriggerEnter2D(Collider2D collider)
	{
		PBase powerup;
		if(powerup = collider.GetComponent<PBase>())
		{
			powerup.OnPickUp(this.gameObject);
		}
	}
}
