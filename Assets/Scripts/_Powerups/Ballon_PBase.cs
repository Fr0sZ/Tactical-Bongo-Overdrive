using UnityEngine;
using System.Collections;

public class Ballon_PBase : PBase
{
	const float GravityMultiplier = 0.7f;

	public GameObject m_ballonRope1;
	public GameObject m_ballonRope2;
	public GameObject m_ballonRope3;
	public GameObject m_ballon;

	public override void OnPickUp (GameObject player)
	{
		GameObject rope1 = Instantiate(m_ballonRope1, player.transform.position + new Vector3(0,1.6f,0), Quaternion.Euler(0,0, 180)) as GameObject;
		rope1.GetComponent<HingeJoint2D>().connectedBody = player.rigidbody2D;
		GameObject rope2 = Instantiate(m_ballonRope2, player.transform.position + new Vector3(0,1.6f,0), Quaternion.Euler(0,0, 180)) as GameObject;
		rope2.GetComponent<HingeJoint2D>().connectedBody = rope1.rigidbody2D;
		GameObject rope3 = Instantiate(m_ballonRope3, player.transform.position + new Vector3(0,1.6f,0), Quaternion.Euler(0,0, 180)) as GameObject;
		rope3.GetComponent<HingeJoint2D>().connectedBody = rope2.rigidbody2D;
		GameObject ballon = Instantiate(m_ballon, player.transform.position + new Vector3(0,1,0), Quaternion.Euler(0,0, 0)) as GameObject;
		ballon.GetComponent<HingeJoint2D>().connectedBody = rope3.rigidbody2D;

		player.rigidbody2D.gravityScale *= GravityMultiplier;

		base.OnPickUp (player);
	}
}
