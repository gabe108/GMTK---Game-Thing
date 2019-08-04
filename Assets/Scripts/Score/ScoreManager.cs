using UnityEngine;

/// <summary>
/// singleton ScoreManager which is responsible for various player stats
/// </summary>
public class ScoreManager : MonoBehaviour
{
    // singleton
    private static ScoreManager m_instance;

    // scores
    private float m_time = 0;
    private int m_deaths = 0;
    private int m_flagsPlanted = 0;

    #region getters
    
    public static ScoreManager GetInstance()
    {
        if (!m_instance)
            m_instance = FindObjectOfType<ScoreManager>();

        Debug.Assert(m_instance, "ScoreManager: Instance wasn't found!");
        return m_instance;
    }

    /// <summary>
    /// get the total playtime
    /// </summary>
    /// <returns>the total play time</returns>
    public float GetPlayTime() { return m_time; }

    /// <summary>
    /// get the total amount of player deaths
    /// </summary>
    /// <returns>the total amount of deaths</returns>
    public int GetDeaths() { return m_deaths; }

    /// <summary>
    /// get the total amount of flags planted
    /// </summary>
    /// <returns>the total amount of flags planted</returns>
    public int GetFlagsPlanted() { return m_flagsPlanted; }

    #endregion

    /// <summary>
    /// increment play time
    /// </summary>
    private void Update()
    {
        m_time += Time.deltaTime;
    }

    /// <summary>
    /// use this when the player dies!
    /// </summary>
    public void IncrementDeaths()
    {
        m_deaths++;
    }

    /// <summary>
    /// use this when the player plants a flag!
    /// </summary>
    public void IncrementFlagsPlanted()
    {
        m_flagsPlanted++;
    }
}