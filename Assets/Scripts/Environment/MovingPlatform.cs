using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
	[SerializeField] private Transform m_thingToMove;
	[SerializeField] private Transform m_positionOne;
	[SerializeField] private Transform m_positionTwo;
	[SerializeField] private float m_speed = 1.0f;

	private float m_startTime;
	private float journeyLength;

	private void Start()
	{
		CalculateDuration();
		m_thingToMove.position = m_positionOne.position;
	}

	private void Update()
	{
		float distCovered = (Time.time - m_startTime) * m_speed;
		float fracJourney = distCovered / journeyLength;

		m_thingToMove.position = Vector3.Lerp(m_positionOne.position, m_positionTwo.position, fracJourney);

		if(m_thingToMove.position == m_positionTwo.position)
		{
			Transform temp = m_positionTwo;
			m_positionTwo = m_positionOne;
			m_positionOne = temp;

			CalculateDuration();
		}
	}

	private void CalculateDuration()
	{
		m_startTime = Time.time;
		journeyLength = Vector3.Distance(m_positionOne.position, m_positionTwo.position);
	}
}
