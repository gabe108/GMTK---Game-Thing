using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator m_animator;

    public void SetJump()
    {
        m_animator.SetTrigger("Jump");
    }

    public void SetLand()
    {
        m_animator.SetTrigger("Land");
    }

    public void SetIsIdle()
    {
        // isn't doing anything
        m_animator.SetBool("IsFalling", false);
        m_animator.SetBool("IsRising", false);
        m_animator.SetBool("IsOnWall", false);
        m_animator.SetBool("IsWalking", false);
    }

    public void SetIsFalling()
    {
        // is falling
        m_animator.SetBool("IsFalling", true);

        // isn't doing anything else
        m_animator.SetBool("IsRising", false);
        m_animator.SetBool("IsOnWall", false);
        m_animator.SetBool("IsWalking", false);
    }

    public void SetIsRising()
    {
        // is rising
        m_animator.SetBool("IsRising", true);

        // isn't doing anything else
        m_animator.SetBool("IsFalling", false);
        m_animator.SetBool("IsOnWall", false);
        m_animator.SetBool("IsWalking", false);
    }

    public void SetIsOnWall()
    {
        // is on wall
        m_animator.SetBool("IsOnWall", true);

        // isn't doing anything else
        m_animator.SetBool("IsFalling", false);
        m_animator.SetBool("IsRising", false);
        m_animator.SetBool("IsWalking", false);
    }

    public void SetIsWalking()
    {
        // is walking
        m_animator.SetBool("IsWalking", true);

        // isn't doing anything else
        m_animator.SetBool("IsFalling", false);
        m_animator.SetBool("IsRising", false);
        m_animator.SetBool("IsOnWall", false);
    }
}
