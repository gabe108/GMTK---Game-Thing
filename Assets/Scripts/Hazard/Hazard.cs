using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.transform.CompareTag("Player"))
		{
			PlayerDeath player = collision.transform.GetComponent<PlayerDeath>();
			player.Die();
		}
	}
}
