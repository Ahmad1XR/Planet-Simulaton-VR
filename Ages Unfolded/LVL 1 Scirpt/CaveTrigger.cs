using UnityEngine;

public class CaveTrigger : MonoBehaviour
{
    [Header("Objects to show when inside")]
    public GameObject objectsToReveal;

    [Header("Dark overlay effect")]
    public GameObject darkOverlay;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (objectsToReveal != null)
                objectsToReveal.SetActive(true);

            if (darkOverlay != null)
                darkOverlay.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (objectsToReveal != null)
                objectsToReveal.SetActive(false);

            if (darkOverlay != null)
                darkOverlay.SetActive(false);
        }
    }
}
