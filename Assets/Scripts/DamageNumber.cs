using UnityEngine;
using System.Collections;

public class DamageNumber : MonoBehaviour {

	const float StartFadeTime = 0.5f;
	const float FadeTime = 1;

	private float m_timer;

	// Update is called once per frame
	void Update () 
	{
		m_timer += Time.deltaTime;

		transform.position += new Vector3(0, Time.deltaTime, 0);

		if(m_timer > StartFadeTime)
		{
			GetComponent<TextMesh>().color = new Color(0,0,0, 1 - (m_timer - StartFadeTime) / FadeTime);
		}

		if(m_timer > StartFadeTime + FadeTime)
			Destroy(gameObject);
	}
}
