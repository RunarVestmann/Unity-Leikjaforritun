using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    float speed;

    [SerializeField]
    float jumpForce;

    [SerializeField]
    GroundCheck groundCheck;

    [SerializeField]
    Collider2D swordCollider;

    Rigidbody2D rb2d;
    Vector2 moveDirection;

    Animator animator;
    bool facingRight = true;
    bool isAttacking = false;

    int currentAnimationState;

    // Animations
    int IDLE;
    int RUN;
    int JUMP;
    int FALL;
    int ATTACK;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        IDLE = Animator.StringToHash("Idle");
        RUN = Animator.StringToHash("Run");
        JUMP = Animator.StringToHash("Jump");
        FALL = Animator.StringToHash("Fall");
        ATTACK = Animator.StringToHash("Attack");
        currentAnimationState = IDLE;
    }


    public void OnMovement(InputAction.CallbackContext context) => moveDirection = context.ReadValue<Vector2>();

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && groundCheck.isGrounded) rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed && !isAttacking)
        {
            isAttacking = true;
            swordCollider.enabled = true;
            Invoke(nameof(StopAttacking), 0.15f);
        }
    }

    void StopAttacking()
    {
        swordCollider.enabled = false;
        isAttacking = false;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("PlayerDamage"))
        {
            print("Player took damage");
        }
    }

    void Update()
    {
        ApplyAnimations();
        ApplySpriteRotation();
    }

    void ApplyAnimations()
    {
        if (isAttacking)
        {
            SetAnimationState(ATTACK);
            return;
        }

        if (groundCheck.isGrounded)
        {
            if (moveDirection.x == 0f) SetAnimationState(IDLE);
            else SetAnimationState(RUN);
        }
        else
        {
            if (rb2d.velocity.y > 0f) SetAnimationState(JUMP);
            else SetAnimationState(FALL);
        }
    }
    void ApplySpriteRotation()
    {
        var localScale = transform.localScale;
        if (moveDirection.x < 0f && facingRight)
        {
            localScale.x = -1f;
            facingRight = false;
            transform.localScale = localScale;
        }
        else if (moveDirection.x > 0f && !facingRight)
        {
            localScale.x = 1f;
            facingRight = true;
            transform.localScale = localScale;
        }
    }

    void SetAnimationState(int state)
    {
        if (currentAnimationState == state) return;
        animator.Play(state);
        currentAnimationState = state;
    }

    void FixedUpdate()
    {
        rb2d.AddForce(Vector2.right * moveDirection.x * speed);
    }
}
