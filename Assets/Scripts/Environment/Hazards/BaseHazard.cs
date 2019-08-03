using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHazard : MonoBehaviour
{
	[Range(0, 1)]
	[SerializeField] protected float m_delay;

	protected float m_timer;
	protected bool m_startTimer;

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public virtual void Evaluate()
	{

	}
}
