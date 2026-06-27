using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorToNextLevel : MonoBehaviour
{
    public Animator doorAnimator;
    public string nextSceneName;

    private bool playerInRange = false;
    private bool doorOpened = false;
    public AudioSource openDoorS;
    private void Update()
    {
        if (playerInRange && !doorOpened)
        {
            if (KeyUIManager.instance != null && KeyUIManager.instance.IsKeyIconVisible())
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    doorAnimator.SetBool("IsOpen", true); 
                    doorOpened = true;
                    openDoorS.Play();
                    Invoke(nameof(LoadNextScene), 2.5f); 
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            playerInRange = false;
    }

    private void LoadNextScene()
    {
        
        PlayerPrefs.SetString("NextLevel", "LoadingScreen 2");

       
        SceneManager.LoadScene("LoadingScene 2"); 
    }
}
