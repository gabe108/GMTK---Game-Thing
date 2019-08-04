using System;
using UnityEngine;

/// <summary>
/// singleton ScoreManager which is responsible for various player stats
/// </summary>
public class ScoreManager : MonoBehaviour
{
    // singleton
    private static ScoreManager m_instance;

	public int m_curTime;
	public int m_highTime;
	public int m_currDeaths;
	public int m_highDeaths;
	public int m_curFlags;
	public int m_highFlags;

    // scores
    private float m_time = 0;
    private int m_deaths = 0;
    private int m_flagsPlanted = 0;
	private EndFlag m_endFlag;
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

	public void SetEndFlag(EndFlag _flag) { m_endFlag = _flag; }

    /// <summary>
    /// increment play time
    /// </summary>
    private void Update()
    {
        m_time += Time.deltaTime;

		if (m_endFlag.m_reachedEnd)
			SetVariablesAndHighScore();
    }

	public void SetVariablesAndHighScore()
	{
		m_curTime = (int)m_time;
		m_currDeaths = m_deaths;
		m_curFlags = m_flagsPlanted;

		if (m_curTime > m_highTime)
			m_highTime = m_curTime;

		if (m_currDeaths > m_highDeaths)
			m_highDeaths = m_currDeaths;

		if (m_curFlags > m_highFlags)
			m_highFlags = m_curFlags;

		PlayerPrefs.SetInt("High Time", m_highTime);
		PlayerPrefs.SetInt("High Deaths", m_highDeaths);
		PlayerPrefs.SetInt("High Flags", m_highFlags);
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