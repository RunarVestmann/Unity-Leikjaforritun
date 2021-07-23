using UnityEngine;
using UnityEngine.Events;

public class Zombie : MonoBehaviour, IDamageable
{
    static bool canDamagePlayer = true;

    [SerializeField] float timeBetweenPlayerDamage;
    [SerializeField] float damageFlashDuration;
    [SerializeField] float speed;
    [SerializeField] int health;

    public UnityEvent onDeath;

    Rigidbody2D rb2d;
    Transform playerTransform;

    Material defaultMaterial;
    Material damageMaterial;
    SpriteRenderer spriteRenderer;
    Animator animator;
    bool isDead = false;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultMaterial = spriteRenderer.material;
        animator = GetComponent<Animator>();
        GetComponent<AudioSource>().time = Random.Range(0f, 56.2f);
        damageMaterial = Resources.Load("DamageMaterial", typeof(Material)) as Material;
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        health -= amount;
        spriteRenderer.material = damageMaterial;
        Invoke(nameof(ResetMaterial), damageFlashDuration);

        if (health <= 0)
        {
            onDeath.Invoke();
            isDead = true;
            animator.Play("Death");
            GetComponent<Collider2D>().enabled = false;
            GetComponent<AudioSource>().enabled = false;
            Destroy(gameObject, 5f);
        }
    }

    void ResetMaterial()
    {
        spriteRenderer.material = defaultMaterial;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("DetectionRadius"))
        {
            playerTransform = collider.transform;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (canDamagePlayer && collision.collider.CompareTag("Player"))
        {
            canDamagePlayer = false;
            Invoke(nameof(EnablePlayerDamage), timeBetweenPlayerDamage);
            var damageable = collision.collider.GetComponent<IDamageable>();
            damageable.TakeDamage(1);
        }
    }

    void EnablePlayerDamage()
    {
        canDamagePlayer = true;
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("DetectionRadius"))
        {
            playerTransform = null;
            rb2d.velocity = Vector2.zero;
        }
    }

    void FixedUpdate()
    {
        if (isDead) return;
        if (playerTransform == null) return;

        var direction = -transform.position + playerTransform.position;
        transform.right = direction;
        rb2d.velocity = direction.normalized * speed;
    }
}
