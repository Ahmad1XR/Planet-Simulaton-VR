using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
public class PlayerHealth : MonoBehaviour
{
    public Image healthFillImage;
    public float maxHealth = 100f;
    public float currentHealth;

    private Animator anim;
    private bool isDead = false;
    public static bool isPlayerDead = false;
    public GameObject GameOverCanvas;

    public Image redDamage; 

    [Header("Audio")]
    public AudioSource DamageSound;
    public AudioSource heartBSound;
    public AudioSource breathSound;

    public float fadeSpeed = 5f;
    void Start()
    {
        isDead = false;
        isPlayerDead = false;
        Time.timeScale = 1f;
        currentHealth = maxHealth;
        UpdateHealthUI();

        anim = GetComponent<Animator>();

        if (GameOverCanvas != null)
            GameOverCanvas.SetActive(false);

        Color c = redDamage.color;
        c.a = 0f;
        redDamage.color = c;

    }

    void Update()
    {
        HeartBeat();
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(10f);
        }
       
    }

    public void TakeDamage(float amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();

        DamageSound.Play();
        HeartBeat();

        if (currentHealth <= 0)
        {
            StartCoroutine(DieCoroutine());
        }
    }
    void HeartBeat()
    {
        Color color = redDamage.color;
        float maxAlpha = 0.33f; 

        if (currentHealth < 50)
        {
            color.a = Mathf.MoveTowards(color.a, maxAlpha, fadeSpeed * Time.deltaTime);

            if (!heartBSound.isPlaying) heartBSound.Play();
            if (!breathSound.isPlaying) breathSound.Play();
        }
        else
        {
            color.a = Mathf.MoveTowards(color.a, 0f, fadeSpeed * Time.deltaTime);

            if (color.a <= 0.01f)
            {
                if (heartBSound.isPlaying) heartBSound.Stop();
                if (breathSound.isPlaying) breathSound.Stop();
            }
        }

        redDamage.color = color;
    }
    IEnumerator DieCoroutine()
    {
        isDead = true;
        isPlayerDead = true;
        Debug.Log("Player died");
        anim.SetTrigger("die");

        Player playerScript = GetComponent<Player>();
        if (playerScript != null)
            playerScript.enabled = false;

        yield return new WaitForSeconds(1.5f);


        GameOverController controller = FindObjectOfType<GameOverController>();
        if (controller != null)
        {
            Debug.Log("Calling ShowGameOver()");
            controller.ShowGameOver();
        }
        else
        {
            Debug.LogWarning("GameOverController not found in the scene.");
        }
    }
    void UpdateHealthUI()
    {
        healthFillImage.fillAmount = currentHealth / maxHealth;
    }
    public void Heal(float amount)
    {
        if (isDead) return;

        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        UpdateHealthUI();
    }

}
