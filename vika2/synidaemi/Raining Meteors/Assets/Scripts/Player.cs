using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] UnityEvent onDeath;
    [SerializeField] float speed;

    Rigidbody2D rb2d;
    Animator animator;
    Vector2 moveDirection;
    bool facingRight = true;
    bool isDead = false;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isDead) return;

        // Reading in move input
        moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);

        // Flip sprite
        Vector3 localScale = transform.localScale;
        if (moveDirection.x < 0f && facingRight)
        {
            facingRight = false;
            localScale.x = -1f;
        }
        else if (moveDirection.x > 0f && !facingRight)
        {
            facingRight = true;
            localScale.x = 1f;
        }
        transform.localScale = localScale;

        // Animation
        if (Mathf.Abs(moveDirection.x) > 0f) animator.Play("Run");
        else animator.Play("Idle");
    }

    void FixedUpdate()
    {
        rb2d.velocity = moveDirection * speed;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (isDead) return;
        if (collider.CompareTag("Meteor"))
        {
            onDeath.Invoke();
            SoundManager.instance.PlayDeathSound();
            isDead = true;
            moveDirection = Vector2.zero;
            animator.Play("Dead");
        }
    }
}
