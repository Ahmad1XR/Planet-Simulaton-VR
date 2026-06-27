using UnityEngine;

public class Sounds : MonoBehaviour
{

    public AudioSource nightSound;
    public AudioSource insideCastleSound;

    public PauseMenu2 pauseMenu; 

    void Start()
    {
        if (nightSound != null)
        {
            nightSound.Play();
            pauseMenu.SetCurrentBackgroundSound(nightSound); 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (nightSound.isPlaying)
                nightSound.Stop();

            if (!insideCastleSound.isPlaying)
                insideCastleSound.Play();

            pauseMenu.SetCurrentBackgroundSound(insideCastleSound); 
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (insideCastleSound.isPlaying)
                insideCastleSound.Stop();

            if (!nightSound.isPlaying)
                nightSound.Play();

            pauseMenu.SetCurrentBackgroundSound(nightSound); 
        }
    }
}



