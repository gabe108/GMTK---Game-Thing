using UnityEngine;

/// <summary>
/// PlayerInput class responsible for handling input and performing player actions
/// accordingly
/// </summary>
public class PlayerInput : MonoBehaviour
{
    [SerializeField] private PlayerMovement m_movement;
    [SerializeField] private PlayerCollision m_playerCollision;

    /// <summary>
    /// error handling
    /// </summary>
    void Start()
    {
        Debug.Assert(m_movement, "PlayerInput: PlayerMovement not assigned!");
        Debug.Assert(m_playerCollision, "PlayerInput: PlayerCollision not assigned!");
    }

    /// <summary>
    /// input checking
    /// </summary>
    void Update()
    {
        if (m_movement)
        {
            float xAxis = Input.GetAxisRaw("Horizontal");

            // perform horizontal movement (Left+Right, A+D)
            if (!m_playerCollision.GetIsOnWall())
                m_movement.Move(xAxis);
            else if (m_playerCollision.GetIsOnLeftWall())
                m_movement.Move(Mathf.Clamp(xAxis, 0, 1));
            else if (m_playerCollision.GetIsOnRightWall())
                m_movement.Move(Mathf.Clamp(xAxis, -1, 0));

            // perform jump (Space)
            if (Input.GetButtonDown("Jump"))
            {
                if (m_playerCollision.GetIsGrounded())
                    m_movement.Jump(Vector2.up);
                else if (m_playerCollision.GetIsOnWall())
                    m_movement.WallJump();
            }
        }
    }
}
