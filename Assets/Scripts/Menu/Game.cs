using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] private GameObject m_pauseMenu;
    [SerializeField] private GameObject m_gameWin;

	private EndFlag m_endFlag;
	private GameObject m_previousMenu;

	[SerializeField] private TextMeshProUGUI m_pauseHighTime;
	[SerializeField] private TextMeshProUGUI m_pauseHighDeaths;
	[SerializeField] private TextMeshProUGUI m_pauseHighFlags;

	[SerializeField] private TextMeshProUGUI m_winHighTime;
	[SerializeField] private TextMeshProUGUI m_winHighDeaths;
	[SerializeField] private TextMeshProUGUI m_winHighFlags;

	private void Start()
	{
		m_endFlag = FindObjectOfType<EndFlag>();
		if (m_endFlag != null)
			Debug.Log("No EndFlag");
	}

	private void Update()
    {
		if (Input.GetKeyDown(KeyCode.Escape) && 
			!(m_gameWin.activeSelf))
			PauseMenu(!(m_pauseMenu.activeSelf));

		if (m_endFlag.m_reachedEnd)
		{
			GameWin();
			m_endFlag.m_reachedEnd = false;
		}
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        m_pauseMenu.SetActive(false);
    }

    public void RestartGame()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene("Game");
    }

    public void Settings()
    {
		m_pauseMenu.SetActive(false);
		m_gameWin.SetActive(false);
    }

    public void ReturnToMenu()
	{
		PlayerPrefs.Save();
		SceneManager.LoadScene("MainMenu");
    }

    public void PauseMenu(bool _active)
	{
		if(_active)
			Time.timeScale = 0f;
		else
			Time.timeScale = 1f;

		m_previousMenu = m_pauseMenu;
		ScoreManager score = ScoreManager.GetInstance();
		score.SetVariablesAndHighScore();

		if (PlayerPrefs.HasKey("High Time"))
			m_pauseHighTime.text = PlayerPrefs.GetInt("High Time").ToString();
		if (PlayerPrefs.HasKey("High Deaths"))
			m_pauseHighDeaths.text = PlayerPrefs.GetInt("High Deaths").ToString();
		if (PlayerPrefs.HasKey("High Flags"))
			m_pauseHighFlags.text = PlayerPrefs.GetInt("High Flags").ToString();

		m_pauseMenu.SetActive(_active);
		m_gameWin.SetActive(false);
    }

	public void GameWin()
	{
		m_winHighTime.text = ScoreManager.GetInstance().m_curTime.ToString();
		m_winHighDeaths.text = ScoreManager.GetInstance().m_currDeaths.ToString();
		m_winHighFlags.text = ScoreManager.GetInstance().m_curFlags.ToString();

		m_previousMenu = m_gameWin;
		m_pauseMenu.SetActive(false);
		m_gameWin.SetActive(true);
	}

	public void Back()
	{
		m_pauseMenu.SetActive(false);
		m_gameWin.SetActive(false);

		m_previousMenu.SetActive(true);
	}

	public void Exit()
	{
		Application.Quit();
	}
}