using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
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
    bool isDead = false;

    Rigidbody2D rb2d;
    Animator animator;

    Material whiteMaterial;
    Material defaultMaterial;


    void Awake()
    {
        // Movement
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = Vector3.down * speed;

        // Set up destruction when off screen
        var spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (spriteRenderer)
        {
            var script = spriteRenderer.gameObject.AddComponent<DestroyWhenInvisible>();
            script.objectToDestroy = gameObject;
        }

        // Set up materials
        defaultMaterial = spriteRenderer.material;
        whiteMaterial = Resources.Load("White", typeof(Material)) as Material;

        // Set up animator
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isDead) return;

        if (timeSinceLastShot < timeBetweenShots)
            timeSinceLastShot += Time.deltaTime;
        else
        {
            timeSinceLastShot = 0f;
            Shoot();
        }
    }

    void Shoot()
    {
        var projectileGameObject = Instantiate(shot, firePoint.position, Quaternion.identity);
        var projectile = projectileGameObject.GetComponent<Projectile>();
        projectile.damageAmount = shotDamage;
        projectile.speed = shotSpeed;
        projectile.colliderToIgnore = GetComponent<Collider2D>();
        projectile.tagsToIgnore = new string[1] { "Enemy" };
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
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

