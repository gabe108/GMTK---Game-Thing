using UnityEngine;

/// <summary>
/// PlayerInput class responsible for handling input and performing player actions
/// accordingly
/// </summary>
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerCollision))]
public class PlayerInput : MonoBehaviour
{
    private PlayerMovement m_movement;
    private PlayerCollision m_playerCollision;
	public Transform m_flagReset;
	public Transform m_flagSpawn;

    /// <summary>
    /// error handling
    /// </summary>
    void Start()
    {
        m_movement = GetComponent<PlayerMovement>();
        m_playerCollision = GetComponent<PlayerCollision>();
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
