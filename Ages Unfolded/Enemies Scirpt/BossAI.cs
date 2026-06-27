using UnityEngine;
using System.Collections;
public class BossAI : MonoBehaviour
{
    public Transform player;
    public float attackRange = 1.5f;
    public int damage = 15;

    public int maxHealth = 100;
    private int currentHealth;

    public GameObject doorToOpen;

    private Animator anim;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private bool isDead = false;
    private float lastAttackTime;
    private float attackCooldown = 2f;

    private Color originalColor;
    public AudioSource dieSound;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (isDead) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            rb.linearVelocity = Vector2.zero;
            anim.SetBool("run", false);

            if (Time.time - lastAttackTime > attackCooldown)
            {
                anim.SetTrigger("attack");
                lastAttackTime = Time.time;

                PlayerHealth ph = player.GetComponent<PlayerHealth>();
                if (ph != null)
                    ph.TakeDamage(damage);
            }

            if (player.position.x > transform.position.x)
                transform.localScale = new Vector3(1, 1, 1);
            else
                transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        StartCoroutine(FlashRed());

        if (currentHealth <= 0)
            Die();
    }

    IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = originalColor;
    }

    void Die()
    {
        isDead = true;
        anim.SetTrigger("death");
        rb.linearVelocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Static;
        GetComponent<Collider2D>().enabled = false;

        if (doorToOpen != null)
            doorToOpen.SetActive(false);
        dieSound.Play();
        Destroy(gameObject, 3f);
    }
    public void DealDamage()
    {
        if (isDead) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= attackRange)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }
}
