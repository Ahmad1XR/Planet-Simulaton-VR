using UnityEngine;

public class SettingPanel : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject gameTitle;
    public GameObject gameRights;
    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
        gameTitle.SetActive(false);
        gameRights.SetActive(false);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        gameTitle.SetActive(true);
        gameRights.SetActive(true);
    }
}
