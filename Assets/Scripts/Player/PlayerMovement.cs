using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public enum WallSide
{
    Left,
    Right
}

public enum PlayerState
{
    Idle,
    Walk,
    Wall,
    Jumping,
    Rising,
    Falling,
    Landing
}

/// <summary>
/// PlayerMovement class responsible for performing horizontal movement and jumps
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerCollision))]
[RequireComponent(typeof(PlayerAnimation))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float m_movementSpeed = 10;

    [Header("Jumping")]
    [SerializeField] private float m_jumpVelocity = 10;
    [SerializeField] private float m_fallMultiplier = 2.5f;
    [SerializeField] private float m_lowJumpMultiplier = 2.0f;

    [Header("Wall Jumping")]
    [SerializeField] private float m_wallJumpLerp = 10;
    private WallSide m_lastWallSide;

    [SerializeField] private UnityEvent m_onJump;

    private PlayerState m_playerState = PlayerState.Idle;
    private PlayerState m_previousState = PlayerState.Idle;

    // helpful bools
    private bool m_canMove = true;
    private static bool m_isAirborne = false;
    private bool m_hasWallJumped = false;

    [SerializeField] private Transform m_sprite;
    private bool m_isFacingRight = false;

    // components
    private Rigidbody2D m_rigidbody2D;
    private PlayerAnimation m_playerAnimation;
    private PlayerCollision m_playerCollision;

    public static bool GetWhetherOrNotThePlayerIsAirborneOrNotForGabe() { return m_isAirborne; }

    /// <summary>
    /// cache required components
    /// </summary>
    private void Start()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        m_playerAnimation = GetComponent<PlayerAnimation>();
        m_playerCollision = GetComponent<PlayerCollision>();
    }

    /// <summary>
    /// 
    /// </summary>
    private void Update()
    {
        if (m_playerCollision.GetIsGrounded())
        {
            m_hasWallJumped = false;
        }
        else if (m_playerCollision.GetIsOnWall())
        {
            m_playerState = PlayerState.Wall;
            WallSlide();
        }

        if (m_previousState != m_playerState)
        {
            m_previousState = m_playerState;
            m_playerAnimation.Reset();

            switch (m_playerState)
            {
                case PlayerState.Idle:
                    m_isAirborne = false;
                    break;

                case PlayerState.Walk:
                    m_playerAnimation.SetIsWalking(true);
                    m_isAirborne = false;
                    break;

                case PlayerState.Wall:
                    m_playerAnimation.SetIsOnWall(true);
                    m_isAirborne = false;
                    break;

                case PlayerState.Rising:
                    m_playerAnimation.SetIsRising(true);
                    break;

                case PlayerState.Falling:
                    m_playerAnimation.SetIsFalling(true);
                    break;
            }
        }

        BetterJumping();
    }

    /// <summary>
    /// use this function to perform player movement!
    /// </summary>
    /// <param name="xAxis">value between -1 and 1 (left and right)</param>
    public void Move(float xAxis)
    {
        if (!m_canMove)
            return;

        if (xAxis != 0)
        {
            if (m_playerCollision.GetIsGrounded())
                m_playerState = PlayerState.Walk;

            if (!m_isFacingRight && xAxis < 0)
                FlipSprite();
            else if (m_isFacingRight && xAxis > 0)
                FlipSprite();
        }
        else
        {
            if (m_playerCollision.GetIsGrounded())
                m_playerState = PlayerState.Idle;
        }

        // perform movement
        if (!m_hasWallJumped)
            m_rigidbody2D.velocity = new Vector2(xAxis * m_movementSpeed, m_rigidbody2D.velocity.y);
        else
            m_rigidbody2D.velocity = Vector2.Lerp(m_rigidbody2D.velocity, (new Vector2(xAxis * m_movementSpeed, m_rigidbody2D.velocity.y)), m_wallJumpLerp * Time.deltaTime);
    }

    /// <summary>
    /// use this to perform a jump!
    /// </summary>
    public void Jump(Vector2 direction)
    {
        m_playerAnimation.SetJump();
        m_onJump.Invoke();

        m_isAirborne = true;

        m_rigidbody2D.velocity = new Vector2(m_rigidbody2D.velocity.x, 0);
        m_rigidbody2D.velocity += direction * m_jumpVelocity;
    }

    /// <summary>
    /// modify jump falling for juiciness
    /// </summary>
    private void BetterJumping()
    {
        // hold jump to fall for longer, or press to fall quicker
        if (m_rigidbody2D.velocity.y < 0)
        {
            Debug.Log("Falling");

            if (!m_playerCollision.GetIsOnWall())
                m_playerState = PlayerState.Falling;

            m_rigidbody2D.velocity += ModifiedGravity(m_fallMultiplier); // hold jump to fall slower
        }
        else if (m_rigidbody2D.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            m_rigidbody2D.velocity += ModifiedGravity(m_lowJumpMultiplier); // or press it quickly for a low jump
        }
        else if (m_rigidbody2D.velocity.y > 0)
        {
            Debug.Log("Rising");

            if (!m_playerCollision.GetIsOnWall())
                m_playerState = PlayerState.Rising;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void WallSlide()
    {
        bool isRightWall = m_playerCollision.GetIsOnRightWall();
        WallSide wallSide = isRightWall ? WallSide.Right : WallSide.Left;

        if (isRightWall)
        {
            if (m_isFacingRight)
                FlipSprite();
        }
        else
        {
            if (!m_isFacingRight)
                FlipSprite();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void WallJump()
    {
        bool isRightWall = m_playerCollision.GetIsOnRightWall();
        WallSide wallSide = isRightWall ? WallSide.Right : WallSide.Left;

        if (m_hasWallJumped && wallSide == m_lastWallSide)
            return;

        Vector2 direction = Vector2.left;
        
        if (isRightWall)
            direction = Vector2.left;
        else
            direction = Vector2.right;

        StartCoroutine(DisableMovement());

        // perform jump
        Jump((Vector2.up / 1.5f) + (direction / 1.5f));

        // can't wall jump again until landed
        m_hasWallJumped = true;
        m_lastWallSide = wallSide;
    }

    #region helper functions

    /// <summary>
    /// helper function which flips a sprite in the x axis
    /// </summary>
    private void FlipSprite()
    {
        m_isFacingRight = !m_isFacingRight;

        // perform flip
        Vector2 scale = m_sprite.localScale;
        scale.x *= -1;
        m_sprite.localScale = scale;
    }

    /// <summary>
    /// helper function which disables movement for an arbritrary amount of time (walljump)
    /// </summary>
    /// <param name="duration">the amount of time to disable movement for</param>
    private IEnumerator DisableMovement(float duration = 0.1f)
    {
        m_canMove = false; // can't move
        yield return new WaitForSeconds(duration); // wait
        m_canMove = true; // can move
    }

    /// <summary>
    /// modified gravity formula
    /// </summary>
    /// <param name="multiplier"></param>
    /// <returns></returns>
    private Vector2 ModifiedGravity(float multiplier)
    {
        return Vector2.up * Physics2D.gravity.y * (multiplier - 1) * Time.deltaTime;
    }

    #endregion
}