using System.Collections;
using TMPro;
using UnityEngine;

public class TypingEffect : MonoBehaviour
{
    public float typingSpeed = 0.05f;
    public AudioSource blipAudioSource;  

    private TextMeshProUGUI textMesh;
    private Coroutine typingCoroutine;
    private string fullText;

    public bool IsTyping { get; private set; } = false;

    private void Awake()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    public IEnumerator ShowText(string text)
    {
        IsTyping = true;
        fullText = text;
        textMesh.text = "";

        if (blipAudioSource != null)
        {
            blipAudioSource.loop = true;
            blipAudioSource.Play();
        }

        foreach (char letter in text)
        {
            textMesh.text += letter;
            yield return new WaitForSecondsRealtime(typingSpeed);
        }

        if (blipAudioSource != null && blipAudioSource.isPlaying)
        {
            blipAudioSource.Stop();
            blipAudioSource.loop = false;
        }

        IsTyping = false;
    }

    public void CompleteText()
    {
        if (IsTyping)
        {
            StopCoroutine(typingCoroutine);
            textMesh.text = fullText;
            IsTyping = false;
            if (blipAudioSource != null && blipAudioSource.isPlaying)
            {
                blipAudioSource.Stop();
                blipAudioSource.loop = false;
            }
        }
    }

    public Coroutine StartTyping(string text)
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(ShowText(text));
        return typingCoroutine;
    }
}
