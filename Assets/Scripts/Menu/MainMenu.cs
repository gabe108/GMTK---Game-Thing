using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject m_mainMenu;
    [SerializeField] private GameObject m_settingsMenu;

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void Menu()
    {
        m_mainMenu.SetActive(true);
        m_settingsMenu.SetActive(false);
    }

    public void Settings()
    {
        m_mainMenu.SetActive(false);
        m_settingsMenu.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
