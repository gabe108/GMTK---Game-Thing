using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTile : MonoBehaviour
{
	private void OnCollisionEnter2D(Collision2D collision)
	{
		collision.transform.parent = this.transform;
	}

	private void OnCollisionStay2D(Collision2D collision)
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			collision.transform.parent = null;
		}
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		collision.transform.parent = null;
	}
}
