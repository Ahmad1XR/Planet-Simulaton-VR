using UnityEngine;
using System.Collections;
public class EnemyAI : MonoBehaviour
{
    public Transform leftPoint;
    public Transform rightPoint;
    public Transform player;

    public float patrolSpeed = 2f;
    public float chaseSpeed = 3.5f;
    public float detectionRange = 5f;
    public float attackRange = 1.2f;
    public int damage = 10;

    public int maxHealth = 45;
    private int currentHealth;

    private Rigidbody2D rb;
    private Animator anim;
    private bool movingRight = true;
    private float attackCooldown = 1.5f;
    private float lastAttackTime;
    private bool isDead = false;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public AudioSource dieSound;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    void Update()
    {
        if (isDead) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        bool playerInDetectionRange = distanceToPlayer <= detectionRange;
        bool playerWithinPatrolArea = player.position.x >= leftPoint.position.x && player.position.x <= rightPoint.position.x;

        if (playerInDetectionRange && playerWithinPatrolArea)
        {
            if (distanceToPlayer <= attackRange)
            {
                rb.linearVelocity = Vector2.zero;
                anim.SetBool("run", false);

                if (Time.time - lastAttackTime > attackCooldown)
                {
                    Debug.Log("Enemy attacking!");
                    anim.SetTrigger("attack");
                    lastAttackTime = Time.time;

                    PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
                    if (playerHealth != null)
                    {
                        playerHealth.TakeDamage(damage);
                    }
                }
            }
            else
            {
                ChasePlayer();
            }
        }
        else
        {
            Patrol();
        }
        anim.SetBool("run", Mathf.Abs(rb.linearVelocity.x) > 0.1f);
    }

    void Patrol()
    {
        if (movingRight)
        {
            rb.linearVelocity = new Vector2(patrolSpeed, rb.linearVelocity.y);
            transform.localScale = new Vector3(1, 1, 1);

            if (transform.position.x >= rightPoint.position.x)
                movingRight = false;
        }
        else
        {
            rb.linearVelocity = new Vector2(-patrolSpeed, rb.linearVelocity.y);
            transform.localScale = new Vector3(-1, 1, 1);

            if (transform.position.x <= leftPoint.position.x)
                movingRight = true;
        }
    }

    void ChasePlayer()
    {
        float direction = player.position.x - transform.position.x;
        rb.linearVelocity = new Vector2(Mathf.Sign(direction) * chaseSpeed, rb.linearVelocity.y);

        if (direction > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else
            transform.localScale = new Vector3(-1, 1, 1);
    }

    public void TakeDamage(int amount)
    {
        Debug.Log($"Enemy took damage: {amount}");
        if (isDead) return;

        currentHealth -= amount;

        StartCoroutine(FlashRed());
        if (currentHealth <= 0)
        {
            Die();
        }

    }
    private IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = originalColor;
    }

    void Die()
    {
        Debug.Log("Die() called");
        isDead = true;
        anim.SetTrigger("death");

        rb.linearVelocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Static;
        GetComponent<Collider2D>().enabled = false;
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
