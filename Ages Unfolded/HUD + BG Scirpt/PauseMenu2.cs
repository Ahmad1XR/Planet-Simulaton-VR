using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu2 : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private bool isPaused = false;
    public GameObject gameTitle;
    public GameObject GameRights;

    [Header("Audio")]
    public AudioSource clickSound;
    private AudioSource currentBgSound;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        gameTitle.SetActive(false);
        GameRights.SetActive(false);
        clickSound.Play();

        if (currentBgSound != null)
            currentBgSound.Play();
    }

    void Pause()
    {
        clickSound.Play();
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        gameTitle.SetActive(true);
        GameRights.SetActive(true);

        if (currentBgSound != null)
            currentBgSound.Pause();
    }

    public void RetryLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartScreen");
    }


    public void SetCurrentBackgroundSound(AudioSource newSound)
    {
        currentBgSound = newSound;
    }
}
