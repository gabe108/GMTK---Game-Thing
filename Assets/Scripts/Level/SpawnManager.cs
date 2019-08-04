using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // singleton
    private static SpawnManager m_instance;

    [Header("Player To Spawn")]
    [SerializeField] private GameObject m_playerPrefab;
    [SerializeField] private DynamicCamera m_camera;

    [Header("Initial Spawn")]
    [SerializeField] private Transform m_startingSpawnPoint;
    private Transform m_flagSpawnPoint;
    private bool m_canPlaceFlag = true;

    #region getters

    public static SpawnManager GetInstance()
    {
        if (!m_instance)
            m_instance = FindObjectOfType<SpawnManager>();

        Debug.Assert(m_instance, "SpawnManager: Instance not found!");
        return m_instance;
    }

    /// <summary>
    /// use this to know whether the player can place a flag or not!
    /// </summary>
    /// <returns>true if they can, false if they can't</returns>
    public bool GetCanPlaceFlag() { return m_canPlaceFlag; }

    /// <summary>
    /// use this get the player spawn point!
    /// </summary>
    /// <returns>if the flag has been set, returns flag transform, otherwise start of level</returns>
    public Transform GetSpawnPoint()
    {
        if (m_flagSpawnPoint != null)
            return m_flagSpawnPoint; // start at last flag location

        // start at beginning of level
        return m_startingSpawnPoint;
    }

    #endregion

    #region setters

    /// <summary>
    /// use this to toggle the ability for the player to place a flag!
    /// </summary>
    /// <param name="canPlaceFlag">true if they can place a flag, false otherwise</param>
    public void SetCanPlaceFlag(bool canPlaceFlag) { m_canPlaceFlag = canPlaceFlag; }

    /// <summary>
    /// use this when placing the flag down, in order to save it's spawn location!
    /// </summary>
    /// <param name="flagSpawnPoint">the location to respawn at</param>
    public void SetFlagSpawnPoint(Transform flagSpawnPoint) { m_flagSpawnPoint = flagSpawnPoint; }
    
    #endregion

    /// <summary>
    /// spawn the player at the starting position
    /// </summary>
    private void Start()
    {
        GameObject player = Instantiate(m_playerPrefab);
        player.transform.position = m_startingSpawnPoint.position;
        m_camera.SetObjectOfInterest(player.transform);
		m_startingSpawnPoint = transform;
	}
}
