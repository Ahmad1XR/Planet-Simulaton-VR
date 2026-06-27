using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public AudioSource takeKeyS;



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            KeyUIManager.instance.ShowKeyIcon();
            Destroy(gameObject);
            takeKeyS.Play();
        }
    }
}
