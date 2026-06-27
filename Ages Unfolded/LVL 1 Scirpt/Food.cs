using UnityEngine;

public class Food : MonoBehaviour
{
    public float floatSpeed = 1f;
    public float floatHeight = 0.5f;
    private Vector3 startPos;
    public AudioSource healSound;
    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.Heal(30);
            }
            healSound.Play();

            Destroy(gameObject);
        }
    }
}