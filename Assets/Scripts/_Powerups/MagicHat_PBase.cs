using UnityEngine;
using System.Collections;

public class MagicHat_PBase : PBase 
{
	private Vector3 HatOffset = new Vector3(-0.4f,0.5f,0);

	public GameObject m_magicHat;

	public override void OnPickUp (GameObject player)
	{
		Vector3 dir = new Vector3(0,0,40) - player.transform.eulerAngles;
		GameObject hat = Instantiate(m_magicHat, transform.position, Quaternion.Euler(dir)) as GameObject;
		hat.transform.parent = player.transform;
		hat.transform.localPosition = HatOffset;
		player.GetComponent<SpriteRenderer>().color -= new Color(0,0,0,1);
		player.transform.FindChild("LeftFoot").GetComponent<SpriteRenderer>().color -= new Color(0,0,0,1);
		player.transform.FindChild("RightFoot").GetComponent<SpriteRenderer>().color -= new Color(0,0,0,1);
		base.OnPickUp (player);
	}
}
