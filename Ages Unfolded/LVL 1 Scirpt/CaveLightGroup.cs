using UnityEngine;

public class CaveLightGroup : MonoBehaviour
{
    public GameObject lightGroup;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            lightGroup.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            lightGroup.SetActive(false);
    }
}
