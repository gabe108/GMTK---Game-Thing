using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crumbles : MonoBehaviour
{
	[Range(0,1)]
	[SerializeField] private float m_delay;

	[SerializeField] private Rigidbody2D m_rigidbody;
	[SerializeField] private float m_timer;

	private bool m_timerStart;

	private void Update()
	{
		if (m_timerStart)
		{
			m_timer += Time.deltaTime;
		}

		if (m_timer > m_delay)
		{
			Drop();
			m_timer = 0f;
			m_timerStart = false;
		}
	}

	private void Drop()
	{
		m_rigidbody.constraints = RigidbodyConstraints2D.None;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		bool m_isPlayer = collision.gameObject.CompareTag("Player") ? true : false;

		if(m_isPlayer)
			m_timerStart = true;
	}
}
