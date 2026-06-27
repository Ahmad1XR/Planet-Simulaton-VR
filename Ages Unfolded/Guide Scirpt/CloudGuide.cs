using UnityEngine;

public class CloudGuide : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float moveDuration = 3f;

    private bool shouldMove = false;
    private float moveTimer;
    public AudioSource windSound;
    void Update()
    {
        if (shouldMove)
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            moveTimer -= Time.deltaTime;

            if (moveTimer <= 0)
            {

                gameObject.SetActive(false);
            }
        }
    }


    public void StartMoving()
    {
        shouldMove = true;
        moveTimer = moveDuration;
        windSound.Play();
    }
    private void OnDisable()
    {
        if (windSound != null && windSound.isPlaying)
        {
            windSound.Stop();
        }
    }
}
