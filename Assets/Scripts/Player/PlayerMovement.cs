using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float m_speed = 1.0f;

    private CharacterController m_characterController;

    /// <summary>
    /// 
    /// </summary>
    private void Start()
    {
        m_characterController = GetComponent<CharacterController>();
    }

    /// <summary>
    /// use this function to perform player movement!
    /// </summary>
    /// <param name="xAxis">value between -1 and 1 (left and right)</param>
    public void Move(float xAxis)
    {
        Vector3 direction = new Vector3(xAxis, 0, 0);

        // perform movement
        m_characterController.Move(direction * m_speed * Time.deltaTime);
    }
}
