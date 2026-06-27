using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private bool isPaused = false;
    public GameObject gameTitle;
    public GameObject GameRights;

    [Header("Audio")]
    public AudioSource clickSound;
    public AudioSource BgSound;
    public AudioSource heartBSound;
 
    void Start()
    {
        if (BgSound != null)
        {
            BgSound.time = 0f; 
            BgSound.Play();
        }
    }
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
        BgSound.Play();
      
        
       
    }

    void Pause()
    {
        clickSound.Play();
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        gameTitle.SetActive(true);
        GameRights.SetActive(true);
        BgSound.Stop();
        

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
}
