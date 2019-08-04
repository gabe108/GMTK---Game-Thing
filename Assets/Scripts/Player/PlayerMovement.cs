using System.Collections;
using UnityEngine;

public enum WallSide
{
    Left,
    Right
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
    private bool m_canMove = true;

    [Header("Jumping")]
    [SerializeField] private float m_jumpVelocity = 10;
    [SerializeField] private float m_fallMultiplier = 2.5f;
    [SerializeField] private float m_lowJumpMultiplier = 2.0f;

    [Header("Wall Jumping")]
    [SerializeField] private float m_wallJumpLerp = 10;
    private WallSide m_lastWallSide;

    //[Header("Wall Sliding")]
    private float m_slideSpeed = 1;
    private IEnumerator m_wallSlideDisabled;

    // helpful bools
    private bool m_isAirborne = false;
    private bool m_hasWallJumped = false;
    private bool m_canWallSlide = true;

    [SerializeField] private Transform m_sprite;
    private bool m_isFacingRight = false;

    private Rigidbody2D m_rigidbody2D;
    private PlayerAnimation m_playerAnimation;
    private PlayerCollision m_playerCollision;

    /// <summary>
    /// 
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
            m_playerAnimation.SetIsOnWall();
            WallSlide();
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
            m_playerAnimation.SetIsWalking();

            if (!m_isFacingRight && xAxis < 0)
                FlipSprite();
            else if (m_isFacingRight && xAxis > 0)
                FlipSprite();
        }
        else
            m_playerAnimation.SetIsIdle();

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
        if (!m_playerCollision.GetIsOnWall())
            m_playerAnimation.SetJump();

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
                m_playerAnimation.SetIsFalling();

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
                m_playerAnimation.SetIsRising();
        }
        else if (m_rigidbody2D.velocity.y == 0)
            m_playerAnimation.SetIsIdle();
    }

    /// <summary>
    /// 
    /// </summary>
    //private void WallSlide()
    //{
    //    if (!m_canWallSlide)
    //        return;

    //    bool isPushingWall = false;

    //    // determine whether the player is trying to push on the wall
    //    if ((m_rigidbody2D.velocity.x > 0 && m_playerCollision.GetIsOnRightWall()) ||
    //        (m_rigidbody2D.velocity.x < 0 && m_playerCollision.GetIsOnLeftWall()))
    //        isPushingWall = true;

    //    Debug.Log("Is Pushing Wall: " + isPushingWall);
    //    // if the player is pushing on the wall, let them slide (0), otherwise let them jump away
    //    //float push = isPushingWall ? 0 : m_rigidbody2D.velocity.x;

    //    // adjust y velocity to simulate sliding
    //    m_rigidbody2D.velocity = new Vector2(m_rigidbody2D.velocity.x, -m_slideSpeed);
    //}

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

    private IEnumerator DisableMovement(float duration = 0.1f)
    {
        m_canMove = false;
        yield return new WaitForSeconds(duration);
        m_canMove = true;
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

    /// <summary>
    /// 
    /// </summary>
    private void FlipSprite()
    {
        m_isFacingRight = !m_isFacingRight;

        Vector2 scale = m_sprite.localScale;
        scale.x *= -1;
        m_sprite.localScale = scale;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="multiplier"></param>
    /// <returns></returns>
    private Vector2 ModifiedGravity(float multiplier)
    {
        return Vector2.up * Physics2D.gravity.y * (multiplier - 1) * Time.deltaTime;
    }
}