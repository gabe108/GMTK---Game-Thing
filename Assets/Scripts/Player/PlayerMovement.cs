using UnityEngine;

/// <summary>
/// PlayerMovement class responsible for performing horizontal movement and jumps
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float m_movementSpeed = 10;

    [Header("Jumping")]
    [SerializeField] private float m_jumpHeight = 1;
    [SerializeField] private float m_gravity = -98;
    [Tooltip("The distance of the raycast which checks whether the player is grounded")]
    [SerializeField] private float m_groundedDistance = 0.1f;

    private CharacterController m_characterController;
    private Vector3 m_velocity;

    /// <summary>
    /// cache CharacterController component
    /// </summary>
    private void Start()
    {
        m_characterController = GetComponent<CharacterController>();
    }

    /// <summary>
    /// apply gravity
    /// </summary>
    private void Update()
    {
        // apply gravity
        m_velocity.y += m_gravity * Time.deltaTime;

        // perform movement
        m_characterController.Move(m_velocity * Time.deltaTime);
    }

    /// <summary>
    /// use this function to perform player movement!
    /// </summary>
    /// <param name="xAxis">value between -1 and 1 (left and right)</param>
    public void Move(float xAxis)
    {
        Vector3 direction = new Vector3(xAxis, 0, 0);

        // perform movement
        m_characterController.Move(direction * m_movementSpeed * Time.deltaTime);

        // face relevant direction
        transform.forward = direction;
    }

    /// <summary>
    /// use this to perform a jump!
    /// </summary>
    public void Jump()
    {
        // give boost in vertical velocity to perform jump
        if (IsGrounded())
            m_velocity.y = Mathf.Sqrt(m_jumpHeight * m_gravity * -2.0f);
    }

    /// <summary>
    /// helper function which performs a Raycast to check if the player is grounded
    /// (or close to grounded)
    /// </summary>
    /// <returns>true if grounded, false if airborne</returns>
    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, m_groundedDistance);
    }
}