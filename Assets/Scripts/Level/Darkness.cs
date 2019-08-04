using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Darkness : MonoBehaviour
{
    [Header("Positional Variables")]
    [SerializeField] private Vector3 m_startOffset;
    [SerializeField] private Vector3 m_direction = Vector3.right;

    [Header("Speeds")]
    [SerializeField] private float m_defaultSpeed = 1;
    [SerializeField] private float m_flagSpeed = 0.5f;

    private void Update()
    {
        transform.position += m_direction * m_defaultSpeed * Time.deltaTime;
    }

    public void ResetDarkness(Transform playerPosition)
    {
        transform.position = playerPosition.position + m_startOffset;
    }
}
