using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndFlag : MonoBehaviour
{
	private Darkness m_darkness;
	public bool m_reachedEnd;

	private void Start()
	{
		m_darkness = FindObjectOfType<Darkness>();
		ScoreManager.GetInstance().SetEndFlag(this);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.transform.CompareTag("Player")
			&& collision.transform.GetComponent<PlayerInput>().m_isCarryingFlag)
		{
			m_darkness.SetSpeed(0f);
			m_reachedEnd = true;
			GetComponent<BoxCollider2D>().enabled = false;
		}
	}
}
