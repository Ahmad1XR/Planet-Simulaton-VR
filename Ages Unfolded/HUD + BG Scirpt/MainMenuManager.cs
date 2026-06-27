using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject settingsPanel; 

 
    public void PlayGame()
    {
        SceneManager.LoadScene("FirstPlace"); 
    }

    
    public void OpenSettings()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(true);
    }

   
    public void CloseSettings()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }


    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }
}
