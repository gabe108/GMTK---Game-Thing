using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crumbles : MonoBehaviour
{
	[Range(0,1)]
	[SerializeField] private float m_delay;
	[Range(0,1)]
	[SerializeField] private float m_DestroyDelay;
	[SerializeField] private Rigidbody2D m_rigidbody;

	private float m_timer;
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

	IEnumerator Drop()
	{
		m_rigidbody.constraints = RigidbodyConstraints2D.None;

		yield return new WaitForSeconds(m_DestroyDelay);

	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		bool m_isPlayer = collision.gameObject.CompareTag("Player") ? true : false;

		if(m_isPlayer)
			m_timerStart = true;
	}
}
