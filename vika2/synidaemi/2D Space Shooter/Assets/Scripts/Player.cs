using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
    [SerializeField] int health;
    [SerializeField] float shotSpeed;
    [SerializeField] int shotDamage;
    [SerializeField] float speed;
    [SerializeField] float timeBetweenShots;
    [SerializeField] GameObject shot;
    [SerializeField] Transform firePoint;
    [SerializeField] SpriteRenderer spriteRenderer;

    float timeSinceLastShot = 0f;
    Rigidbody2D rb2d;
    bool isDead = false;
    Animator animator;
    Vector2 moveDirection;

    Material whiteMaterial;
    Material defaultMaterial;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();

        // Set up materials
        defaultMaterial = spriteRenderer.material;
        whiteMaterial = Resources.Load("White", typeof(Material)) as Material;

        // Set up animator
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isDead) return;

        moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (timeSinceLastShot < timeBetweenShots)
            timeSinceLastShot += Time.deltaTime;
        else if (Input.GetKey(KeyCode.Space))
        {
            timeSinceLastShot = 0f;
            var projectileGameObject = Instantiate(shot, firePoint.position, Quaternion.identity);
            var projectile = projectileGameObject.GetComponent<Projectile>();
            projectile.damageAmount = shotDamage;
            projectile.speed = shotSpeed;
            projectile.colliderToIgnore = GetComponent<Collider2D>();
        }
    }

    void FixedUpdate()
    {
        rb2d.velocity = moveDirection.normalized * speed;
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        health -= amount;
        if (health <= 0)
        {
            isDead = true;
            rb2d.velocity = Vector2.zero;
            animator.Play("Explosion");
            GetComponent<Collider2D>().enabled = false;
            Destroy(gameObject, 2f);
        }
        else
        {
            // Make sprite white
            spriteRenderer.material = whiteMaterial;
            Invoke(nameof(ResetMaterial), 0.4f);
        }
    }

    void ResetMaterial()
    {
        spriteRenderer.material = defaultMaterial;
    }
}

