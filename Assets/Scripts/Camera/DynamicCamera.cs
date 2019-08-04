using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class DynamicCamera : MonoBehaviour
{
    private class XYBounds
    {
        public Vector2 min, max;

        public XYBounds(float xMin, float xMax, float yMin, float yMax)
        {
            min.x = xMin;
            min.y = yMin;
            max.x = xMax;
            max.y = yMax;
        }
    }

    [SerializeField] private Vector3 m_offset;

    // camera reference
    private Camera m_camera;

    // objects to follow
    private Transform m_objectToFollow;
    private Vector3 m_velocity;
    //private List<Transform> m_objectsOfInterest;

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

    private void LateUpdate()
    {
        if (m_objectToFollow)
            Move();
    }

    private void Move()
    {
        Vector3 centrePoint = m_objectToFollow.position;
        centrePoint += m_offset;

        m_camera.transform.position = Vector3.SmoothDamp(m_camera.transform.position, centrePoint, ref m_velocity, 0.5f);
    }

    //private Vector2 GetCentrePoint()
    //{
    //    XYBounds bounds = GetXYBounds();

    //    float xCentre = (bounds.min.x + bounds.max.x) * 0.5f;
    //    float yCentre = (bounds.min.y + bounds.max.y) * 0.5f;

    //    return new Vector2(xCentre, yCentre);
    //}

    //private XYBounds
}
