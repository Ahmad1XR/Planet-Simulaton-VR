using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOverController : MonoBehaviour
{
    public GameObject gameOverCanvas;
    public AudioSource Gsound;
    private void Start()
    {
        if (gameOverCanvas != null)
            gameOverCanvas.SetActive(false); 
    }

    public void ShowGameOver()
    {
        if (gameOverCanvas != null)
            gameOverCanvas.SetActive(true); 
        Gsound.Play();

        Time.timeScale = 0f; 
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Gsound.Stop();
    }

    public void Quit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartScreen");
        Gsound.Stop();
    }
}
