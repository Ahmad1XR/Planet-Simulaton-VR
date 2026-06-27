using Unity.VisualScripting;
using UnityEngine;

public class DoorDisplay : MonoBehaviour
{
    public float fadeDuration = 4f;  
    public float delayBeforeFade = 3f; 

    private SpriteRenderer[] renderers;
    private float fadeTimer;
    private bool isFading = false;
    public AudioSource closeDoorS;
    private void Start()
    {
        renderers = GetComponentsInChildren<SpriteRenderer>();

        StartCoroutine(StartFadeAfterDelay());
        closeDoorS.Play();
    }

    private System.Collections.IEnumerator StartFadeAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeFade); 
        isFading = true;
        fadeTimer = fadeDuration;
    }

    private void Update()
    {
        if (!isFading) return;

        fadeTimer -= Time.deltaTime;
        float alpha = fadeTimer / fadeDuration;

        foreach (var rend in renderers)
        {
            Color color = rend.color;
            color.a = Mathf.Clamp01(alpha);
            rend.color = color;
        }

        if (fadeTimer <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
