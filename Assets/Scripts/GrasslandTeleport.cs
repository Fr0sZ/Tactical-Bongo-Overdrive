using UnityEngine;
using System.Collections;

public class GrasslandTeleport : MonoBehaviour
{
    public Transform spawn;

    void OnTriggerEnter2D(Collider2D other)
    {
        //if (other.CompareTag("Player"))
		//{
			float tempy = spawn.gameObject.transform.position.y; // Change Y to spawn pos
			float tempx = other.gameObject.transform.position.x; // Use Same X
			other.gameObject.transform.position = new Vector2(tempx, tempy);
			//Debug.Log("TP: " + other.tag + " X:" + tempx + " Y:" + tempy);
        //}
    }

}