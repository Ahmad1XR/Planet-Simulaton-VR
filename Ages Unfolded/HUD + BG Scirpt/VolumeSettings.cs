using UnityEngine;
using UnityEngine.UI;
public class VolumeSettings : MonoBehaviour
{
    public Slider volumeSlider; 

    private void Start()
    {
     
        if (!PlayerPrefs.HasKey("GameVolume"))
        {
            PlayerPrefs.SetFloat("GameVolume", 1f);
        }

        
        float savedVolume = PlayerPrefs.GetFloat("GameVolume");
        volumeSlider.value = savedVolume;

 
        AudioListener.volume = savedVolume;

        
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("GameVolume", volume); 
    }
}
