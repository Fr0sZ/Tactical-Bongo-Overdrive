using UnityEngine;
using System.Collections;

public abstract class WBase : MonoBehaviour {
	public Sprite WTexture;
	public float  FireSpeed;
	private abstract void Start();
	private abstract void Update();
	public abstract void Fire();
}
