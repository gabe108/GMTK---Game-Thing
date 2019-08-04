using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : BaseHazard
{
	private Animator m_anim;
	private void Start()
	{
		m_anim = transform.GetComponent<Animator>(); ;
		if (m_anim != null)
		{
			m_anim.SetBool(0, false);
		}
	}

	public override void Evaluate()
	{
		Transform spikes = transform;

		if (spikes != null)
		{
			if (m_anim != null)
			{
				m_anim.SetBool("PlayAnim", true);
				StartCoroutine(ResetAnim());
			}
		}
	}

	IEnumerator ResetAnim()
	{
		yield return new WaitForSeconds(1f);
		if (m_anim != null)
		{
			m_anim.SetBool("PlayAnim", false);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.transform.CompareTag("Player"))
		{
			collision.transform.GetComponent<PlayerDeath>().Die();
		}
	}
}
