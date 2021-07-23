using UnityEngine;

public class Goblin : Enemy
{
    [SerializeField]
    float timeBetweenWalks;

    [SerializeField]
    float speed;

    [SerializeField]
    Rigidbody2D rb2d;

    [SerializeField]
    float distanceToAttack;

    float timeSinceLastWalk = 0f;
    Vector2 moveDirection = Vector2.right;

    int RUN;
    int ATTACK;

    bool isAttacking = false;
    float startSpeed;
    Transform playerTransform;
    bool facingRight = true;

    protected new void Awake()
    {
        base.Awake();
        RUN = Animator.StringToHash("GoblinRun");
        ATTACK = Animator.StringToHash("GoblinAttack");
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        startSpeed = speed;
    }

    void Update()
    {
        if (!isDead && !isBeingHit)
        {
            if (!isAttacking)
            {
                speed = startSpeed;
                if (Vector2.Distance(transform.position, playerTransform.position) <= distanceToAttack)
                {
                    if (playerTransform.position.x < transform.position.x && facingRight)       ChangeFacingDirection(false);
                    else if (playerTransform.position.x > transform.position.x && !facingRight) ChangeFacingDirection(true);
                    isAttacking = true;
                    SetAnimationState(ATTACK);
                    speed = 0f;
                    Invoke(nameof(StopAttacking), animationDurations[ATTACK]);
                }
                else SetAnimationState(RUN);
            }
        }

        if (isAttacking)
        {
            return;
        }

        if (timeSinceLastWalk >= timeBetweenWalks)
        {
            timeSinceLastWalk = 0f;
            moveDirection = -moveDirection;
            if (moveDirection.x > 0f) ChangeFacingDirection(true);
            else if (moveDirection.x < 0f) ChangeFacingDirection(false);
            return;
        }
        timeSinceLastWalk += Time.deltaTime;
    }

    void ChangeFacingDirection(bool facingRight)
    {
        this.facingRight = facingRight;
        var localScale = transform.localScale;
        if (facingRight) localScale.x = 1f;
        else localScale.x = -1f;
        transform.localScale = localScale;
    }

    void StopAttacking()
    {
        isAttacking = false;
        if (moveDirection.x > 0f) ChangeFacingDirection(true);
        else if (moveDirection.x < 0f) ChangeFacingDirection(false);
    }

    void FixedUpdate() => rb2d.velocity = moveDirection * speed;
}
