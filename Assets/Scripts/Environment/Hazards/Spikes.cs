using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : BaseHazard
{
	[SerializeField] private Transform m_thingToMove;
	[SerializeField] private Transform m_positionOne;
	[SerializeField] private Transform m_positionTwo;
	[SerializeField] private float m_speed = 1.0f;

	private PlayerDeath m_playerDeath;
	private float m_startTime;
	private float m_journeyLength;
	private int m_looped;

	// Start is called before the first frame update
	void Start()
    {
		CalculateDuration();
		m_thingToMove.position = m_positionOne.position;
	}

    // Update is called once per frame
    void Update()
    {
        if(m_startTimer)
		{
			m_timer += Time.deltaTime;
		}

		if (m_timer > m_delay)
		{
			m_playerDeath.Die();
		}
	}

	public override void Evaluate()
	{
		float distCovered = (Time.time - m_startTime) * m_speed;
		float fracJourney = distCovered / m_journeyLength;

		m_thingToMove.position = Vector3.Lerp(m_positionOne.position, m_positionTwo.position, fracJourney);

		if (m_thingToMove.position == m_positionTwo.position)
		{
			Transform temp = m_positionTwo;
			m_positionTwo = m_positionOne;
			m_positionOne = temp;

			CalculateDuration();
			m_looped++;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			m_startTimer = true;
			m_playerDeath = collision.gameObject.GetComponent<PlayerDeath>();
		}
	}

	private void CalculateDuration()
	{
		m_startTime = Time.time;
		m_journeyLength = Vector3.Distance(m_positionOne.position, m_positionTwo.position);
	}
}
