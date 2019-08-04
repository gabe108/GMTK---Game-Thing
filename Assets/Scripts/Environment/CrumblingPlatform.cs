using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrumblingPlatform : MonoBehaviour
{
	[Range(1,2)]
	[SerializeField] private float m_offset;
	private List<Transform> m_crumbles = new List<Transform>();

	private void Start()
	{
		for(int i = 0; i < this.transform.childCount; i++)
		{
			m_crumbles.Add(this.transform.GetChild(i));
		}
		BoxCollider2D collider = m_crumbles[0].gameObject.GetComponent<BoxCollider2D>();
		int width = (int)(collider.bounds.extents.x * 2);
		for(int i = 0; i < m_crumbles.Count; i++)
		{
			Vector3 temp = Vector3.zero;
			temp.x = (width + m_offset) * i;
			m_crumbles[i].position += temp;
			m_crumbles[i].GetComponent<Crumbles>().SetInitialPos(m_crumbles[i].localPosition);
		}
	}
}
