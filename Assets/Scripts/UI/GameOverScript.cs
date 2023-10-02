using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverScript : MonoBehaviour
{
    [SerializeField] GameObject m_background;
    [SerializeField] GameObject m_button;


    private void OnEnable()
    {
        
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOverCall()
    {
        Debug.Log("test");

        m_background.SetActive(true);
        m_button.SetActive(true);
    }
}
