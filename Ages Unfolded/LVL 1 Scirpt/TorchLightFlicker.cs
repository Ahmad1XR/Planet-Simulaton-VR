using UnityEngine;

public class TorchLightFlicker : MonoBehaviour
{
    Vector3 originalScale;
    public float flickerSpeed = 3f;
    public float flickerAmount = 0.05f;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        float noise = Mathf.PerlinNoise(Time.time * flickerSpeed, 0f);
        float scaleFactor = 1f + (noise - 0.5f) * flickerAmount;
        transform.localScale = originalScale * scaleFactor;
    }
}
