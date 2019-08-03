using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] private GameObject m_pauseMenu;
    [SerializeField] private GameObject m_settingsMenu;
    [SerializeField] private GameObject m_gameOver;
    [SerializeField] private GameObject m_gameWin;

	private GameObject m_previousMenu;
    
    private void Update()
    {
		if (Input.GetKeyDown(KeyCode.Escape) && 
			!(m_gameWin.activeSelf) && 
			!(m_gameOver.activeSelf))
			PauseMenu(!(m_pauseMenu.activeSelf));
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        m_pauseMenu.SetActive(false);
        m_settingsMenu.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void Settings()
    {
		m_pauseMenu.SetActive(false);
		m_gameOver.SetActive(false);
		m_gameWin.SetActive(false);

		m_settingsMenu.SetActive(true);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void PauseMenu(bool _active)
    {
		m_previousMenu = m_pauseMenu;

		m_pauseMenu.SetActive(_active);
		m_gameOver.SetActive(false);
		m_gameWin.SetActive(false);
		m_settingsMenu.SetActive(false);
    }

	public void GameOver()
	{
		m_previousMenu = m_gameOver;

		m_settingsMenu.SetActive(false);
		m_pauseMenu.SetActive(false);
		m_gameWin.SetActive(false);

		m_gameOver.SetActive(true);
	}

	public void GameWin()
	{
		m_previousMenu = m_gameWin;

		m_settingsMenu.SetActive(false);
		m_pauseMenu.SetActive(false);
		m_gameOver.SetActive(false);

		m_gameWin.SetActive(true);
	}

	public void Back()
	{
		m_settingsMenu.SetActive(false);
		m_pauseMenu.SetActive(false);
		m_gameOver.SetActive(false);
		m_gameWin.SetActive(false);

		m_previousMenu.SetActive(true);
	}
}
