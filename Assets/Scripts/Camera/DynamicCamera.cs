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

    //speed scale scale
    public float scale;

    //distance to object
    private float distance;

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

    private void FixedUpdate()
    {
        distance = Vector3.Distance(transform.position, m_objectToFollow.position);
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
        m_camera.transform.position = Vector3.SmoothDamp(m_camera.transform.position, centrePoint, ref m_velocity, distance*scale);
    }
}