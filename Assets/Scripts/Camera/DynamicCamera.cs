using UnityEngine;

[RequireComponent(typeof(Camera))]
public class DynamicCamera : MonoBehaviour
{
    [SerializeField] private Vector3 m_offset;
    private Vector3 m_velocity;

    // camera reference
    private Camera m_camera;

    // objects to follow
    private Transform m_objectToFollow;

    #region setters

    public void SetObjectOfInterest(Transform objectOfInterest) { m_objectToFollow = objectOfInterest; }

    #endregion

    /// <summary>
    /// cache camera reference
    /// </summary>
    void Start()
    {
        m_camera = GetComponent<Camera>();
    }

    /// <summary>
    /// perform delayed follow
    /// </summary>
    private void LateUpdate()
    {
        if (m_objectToFollow)
            Move();
    }

    /// <summary>
    /// perform follow
    /// </summary>
    private void Move()
    {
        // determine desired position
        Vector3 centrePoint = m_objectToFollow.position;
        centrePoint += m_offset;

        // smoothly lerp to desired position
        m_camera.transform.position = Vector3.SmoothDamp(m_camera.transform.position, centrePoint, ref m_velocity, 0.5f);
    }
}