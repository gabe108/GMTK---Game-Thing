using UnityEngine;

/// <summary>
/// PlayerInput class responsible for handling input and performing player actions
/// accordingly
/// </summary>
public class PlayerInput : MonoBehaviour
{
    [SerializeField] private PlayerMovement m_movement;

    /// <summary>
    /// error handling
    /// </summary>
    void Start()
    {
        Debug.Assert(m_movement, "PlayerInput: PlayerMovement not assigned!");
    }

    /// <summary>
    /// input checking
    /// </summary>
    void Update()
    {
        if (m_movement)
        {
            // perform horizontal movement (Left+Right, A+D)
            if (Input.GetAxisRaw("Horizontal") != 0)
                m_movement.Move(Input.GetAxisRaw("Horizontal"));

            // perform jump (Space)
            if (Input.GetKeyDown(KeyCode.Space))
                m_movement.Jump();
        }
    }
}
