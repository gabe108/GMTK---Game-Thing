using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
	private PlayerInput m_parent;
	private float m_timer;
	private Transform m_initialPos;
	private bool m_startTimer;

	private void Start()
	{
		m_initialPos = transform;
	}

	// Update is called once per frame
	void Update()
	{
		if (m_startTimer)
		{
			m_timer += Time.deltaTime;
		}

		// TODO once per death placement
		if (Input.GetKeyDown(KeyCode.E) && m_parent != null && m_timer > 0.5f)
		{
			//TODO bounce Things cuz Bruno said so

			Vector3 pos = m_parent.m_flagReset.position;
			gameObject.transform.SetParent(null);
			gameObject.transform.SetPositionAndRotation(pos, Quaternion.identity);
			m_timer = 0f;
			m_startTimer = false;
			m_parent = null;
			SpawnManager.GetInstance().SetFlagSpawnPoint(transform);
			SpawnManager.GetInstance().SetCanPlaceFlag(false);
			ScoreManager.GetInstance().IncrementFlagsPlanted();
		}
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (Input.GetKeyDown(KeyCode.E) && collision.transform.CompareTag("Player")
			&& SpawnManager.GetInstance().GetCanPlaceFlag())
		{
			m_parent = collision.gameObject.GetComponent<PlayerInput>();

			if (m_parent != null)
			{
				Vector3 pos = m_parent.m_flagSpawn.position;
				gameObject.transform.SetPositionAndRotation(pos, Quaternion.identity);
				gameObject.transform.SetParent(m_parent.transform);
				SpawnManager.GetInstance().SetFlagSpawnPoint(null);
				m_startTimer = true;
			}
		}
	}
}
