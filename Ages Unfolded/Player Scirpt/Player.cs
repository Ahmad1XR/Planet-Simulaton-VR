using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float jumpForce = 10f;
    public float speed;
    private bool grounded;
    private bool jumpBlocked = false;

    private Rigidbody2D body;
    private Animator anim;

    [Header("Attack")]
    public int attackDamage = 15;
    public float attackRange = 1f;
    public Transform attackPointRight;
    public Transform attackPointLeft;
    public LayerMask enemyLayer;

    

    [Header("Audio")]
    public AudioSource footstepAudio;
    private bool isRunningSoundPlaying = false;
    public AudioSource attackAudio;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);


        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        
        if (Input.GetKey(KeyCode.Space) && grounded)
            Jump();

        AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);

        
        if (!state.IsName("attack"))
        {
            bool isMoving = Mathf.Abs(body.linearVelocity.x) > 0.1f;
            anim.SetBool("run", isMoving);


            if (isMoving && grounded)
            {
                if (!footstepAudio.isPlaying)
                    footstepAudio.Play();
            }
            else
            {
                if (footstepAudio.isPlaying)
                    footstepAudio.Stop();
            }

            anim.SetBool("grounded", grounded);

            HandleAttack();

            if (horizontalInput == 0)
                anim.SetTrigger("Idle");
        }
    }

    private void HandleAttack()
    {
        if (PlayerHealth.isPlayerDead) return;

        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("attack");
      

            Transform attackPoint = transform.localScale.x > 0 ? attackPointRight : attackPointLeft;

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

            foreach (Collider2D enemyCollider in hitEnemies)
            {
                EnemyAI enemy = enemyCollider.GetComponent<EnemyAI>();
                if (enemy != null)
                {
                    enemy.TakeDamage(attackDamage);
                    continue;
                }

                BossAI boss = enemyCollider.GetComponent<BossAI>();
                if (boss != null)
                {
                    boss.TakeDamage(attackDamage);
                }
            }
        }
    }
    public void PlayAttackSound()
    {
        if (attackAudio != null)
            attackAudio.Play();
    }
    private void Jump()
    {
        if (!grounded || jumpBlocked) return;

        
        if (Mathf.Abs(body.linearVelocity.y) > 0.01f) return;

        body.linearVelocity = new Vector2(body.linearVelocity.x, 0f);
        body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        anim.SetTrigger("jump");
        grounded = false;
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            grounded = true;
    }

   

    private void OnDrawGizmosSelected()
    {
        if (attackPointRight != null)
            Gizmos.DrawWireSphere(attackPointRight.position, attackRange);
        if (attackPointLeft != null)
            Gizmos.DrawWireSphere(attackPointLeft.position, attackRange);
    }
}
