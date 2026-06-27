using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;
public class GuideDialogue : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public TypingEffect typingEffect;
    public AudioSource blipAudioSource;

    public string[] dialogueLines;
    private int currentLineIndex = 0;

    private bool isDialogueActive = false;
    public CloudGuide cloudGuide;
    private void StartDialogue()
    {
        dialoguePanel.SetActive(true);
        isDialogueActive = true;
        currentLineIndex = 0;
        typingEffect.StartTyping(dialogueLines[currentLineIndex]);
        Time.timeScale = 0f; 
    }

    private void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        isDialogueActive = false;
        Time.timeScale = 1f;
        if (cloudGuide != null)
        {
            cloudGuide.StartMoving(); 
        }
    }

    private void Update()
    {
        if (!isDialogueActive) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (typingEffect.IsTyping)
            {
                typingEffect.CompleteText(); 
            }
            else
            {
                currentLineIndex++;
                if (currentLineIndex < dialogueLines.Length)
                {
                    typingEffect.StartTyping(dialogueLines[currentLineIndex]);
                }
                else
                {
                    EndDialogue();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isDialogueActive)
        {
            StartDialogue();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            EndDialogue();
        }
    }
}
