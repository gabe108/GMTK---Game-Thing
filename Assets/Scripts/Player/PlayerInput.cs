using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private PlayerMovement m_movement;

    // Start is called before the first frame update
    void Start()
    {
        if (m_movement == null)
            Debug.LogWarning("PlayerInput: PlayerMovement not assigned!");

        
    }

    /// <summary>
    /// 
    /// </summary>
    void Update()
    {
        m_movement.Move(Input.GetAxisRaw("Horizontal"));
    }
}
