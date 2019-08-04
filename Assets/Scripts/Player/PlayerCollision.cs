using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [Header("Collision")]
    [SerializeField] private Vector2 m_bottomOffset;
    [SerializeField] private Vector2 m_leftOffset;
    [SerializeField] private Vector2 m_rightOffset;
    [SerializeField] private float m_collisionRadius;

    [Header("Layer Mask")]
    [SerializeField] private LayerMask m_groundLayer;
    
    private bool m_isGrounded;
    private bool m_isOnRightWall;
    private bool m_isOnLeftWall;

    #region getters

    public bool GetIsGrounded() { return m_isGrounded; }
    public bool GetIsOnWall() { return m_isOnLeftWall || m_isOnRightWall; }
    public bool GetIsOnLeftWall() { return m_isOnLeftWall; }
    public bool GetIsOnRightWall() { return m_isOnRightWall; }

    #endregion

    /// <summary>
    /// 
    /// </summary>
    void Update()
    {
        Vector2 position = (Vector2)transform.position;

        // detect ground collision
        m_isGrounded = Physics2D.OverlapCircle(position + m_bottomOffset, m_collisionRadius, m_groundLayer);

        // detect wall collision
        m_isOnLeftWall = Physics2D.OverlapCircle(position + m_leftOffset, m_collisionRadius, m_groundLayer);
        m_isOnRightWall =  Physics2D.OverlapCircle(position + m_rightOffset, m_collisionRadius, m_groundLayer);
    }
}
