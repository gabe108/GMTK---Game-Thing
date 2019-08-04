using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator m_animator;

    public void Reset()
    {
        // isn't doing anything
        m_animator.SetBool("IsFalling", false);
        m_animator.SetBool("IsRising", false);
        m_animator.SetBool("IsOnWall", false);
        m_animator.SetBool("IsWalking", false);
    }

    public void SetJump()
    {
        m_animator.SetTrigger("Jump");
    }

    public void SetLand()
    {
        m_animator.SetTrigger("Land");
    }

    public void SetIsFalling(bool value)
    {
        m_animator.SetBool("IsFalling", value);
    }

    public void SetIsRising(bool value)
    {
        m_animator.SetBool("IsRising", value);
    }

    public void SetIsOnWall(bool value)
    {
        m_animator.SetBool("IsOnWall", value);
    }

    public void SetIsWalking(bool value)
    {
        m_animator.SetBool("IsWalking", value);
    }
}
