using UnityEngine;
using UnityEngine.UI;

public class BrightnessController : MonoBehaviour
{
    public Image darkOverlay;
    public Slider brightnessSlider;

    

    void Start()
    {
        float savedBrightness = PlayerPrefs.GetFloat("Brightness", 1f);
        brightnessSlider.value = savedBrightness;
        UpdateBrightness(savedBrightness);

        brightnessSlider.onValueChanged.AddListener(UpdateBrightness);
    }

    public void UpdateBrightness(float value)
    {
        PlayerPrefs.SetFloat("Brightness", value);
        PlayerPrefs.Save();

        float alpha = 1f - value;

        if (alpha > 0.01f)
        {
            if (!darkOverlay.gameObject.activeSelf)
                darkOverlay.gameObject.SetActive(true);

            darkOverlay.color = new Color(0f, 0f, 0f, alpha);
        }
        else
        {
            if (darkOverlay.gameObject.activeSelf)
                darkOverlay.gameObject.SetActive(false);
        }
    } 
}
