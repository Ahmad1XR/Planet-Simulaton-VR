using UnityEngine;

public class CaveButton : MonoBehaviour
{
    [SerializeField] private GameObject doorToOpen;
    private bool activated = false;
    public AudioSource pushButtonS;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!activated && other.CompareTag("Player"))
        {
            activated = true;

            if (doorToOpen != null)
            {
                doorToOpen.SetActive(false); 
                Debug.Log("the door is opening!");
            }
            pushButtonS.Play() ;
            GetComponent<SpriteRenderer>().color = Color.green;
        }
    }
}
