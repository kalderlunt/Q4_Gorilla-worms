using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    /*private void Start()
    {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 165;
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Scenes/Level1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }*/
}
