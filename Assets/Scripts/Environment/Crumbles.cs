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
	private Color m_material;
	private Vector3 m_initialPosition;
	private Renderer m_renderer;
	private BoxCollider2D m_collider;

	public void SetInitialPos(Vector3 _pos) { m_initialPosition = _pos; }

	private void Start()
	{
		m_renderer = m_rigidbody.GetComponent<Renderer>();
		m_collider = m_rigidbody.GetComponent<BoxCollider2D>();
	}

	private void Update()
	{
		if (m_timerStart)
		{
			m_timer += Time.deltaTime;
		}

		if (m_timer > m_delay)
		{
			StartCoroutine("Drop");
			m_timer = 0f;
			m_timerStart = false;
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		bool m_isPlayer = collision.gameObject.CompareTag("Player") ? true : false;

		if(m_isPlayer)
			m_timerStart = true;
	}

	IEnumerator Drop()
	{
		m_rigidbody.constraints = RigidbodyConstraints2D.None | 
								  RigidbodyConstraints2D.FreezePositionX | 
								  RigidbodyConstraints2D.FreezeRotation;

		if (m_collider.isActiveAndEnabled)
			m_collider.enabled = false;

		m_material = m_renderer.material.color;

		StartCoroutine(FadeTo(0.0f, m_DestroyDelay));
		yield return new WaitForSeconds(m_DestroyDelay);

		m_collider.enabled = true;
		StartCoroutine(FadeTo(1.0f, m_DestroyDelay));
		transform.localPosition = m_initialPosition;
		m_rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
	}

	IEnumerator FadeTo(float aValue, float aTime)
	{
		float alpha = m_material.a;
		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
		{
			Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, aValue, t));
			m_renderer.material.color = newColor;
			yield return null;
		}
	}
}
