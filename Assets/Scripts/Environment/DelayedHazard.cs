using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedHazard : MonoBehaviour
{
	[Range(0,1)]
	[SerializeField] private float m_delay;
	[SerializeField] private BaseHazard m_Hazard;

	private float m_timer;
	private bool m_startTimer;
	
	private void Start()
	{
		
	}

	private void Update()
	{
		if(m_startTimer)
		{
			m_timer += Time.deltaTime;
		}

		if(m_timer > m_delay)
		{
			m_startTimer = false;
			m_timer = 0f;
			m_Hazard.Evaluate();
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.transform.CompareTag("Player"))
			m_startTimer = true;
	}
}
