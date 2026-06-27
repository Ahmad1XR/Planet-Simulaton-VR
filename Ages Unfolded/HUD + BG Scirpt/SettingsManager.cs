using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public Slider volumeSlider;
    public Slider brightnessSlider;

    public AudioSource audioSource; 

    private float defaultVolume = 1f;
    private float defaultBrightness = 1f;

    void Start()
    {
  
        volumeSlider.value = defaultVolume;
        brightnessSlider.value = defaultBrightness;

      
        volumeSlider.onValueChanged.AddListener(SetVolume);
        brightnessSlider.onValueChanged.AddListener(SetBrightness);
    }

    public void SetVolume(float volume)
    {
        
        if (audioSource != null)
            audioSource.volume = volume;
   
    }

    public void SetBrightness(float brightness)
    {
        
        RenderSettings.ambientIntensity = brightness;
    }

    public void ResetSettings()
    {
        volumeSlider.value = defaultVolume;
        brightnessSlider.value = defaultBrightness;
    }
}
