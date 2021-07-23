using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] float damageFlashDuration;
    [SerializeField] float bulletSpeed;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] float speed;
    [SerializeField] int maxAmmo;
    [SerializeField] float reloadTime;
    [SerializeField] int health;
    [SerializeField] UnityEvent<int> onHealthChange;
    [SerializeField] float timeBetweenShots;

    float timeSinceLastShot = 0f;
    Animator animator;
    int currentAmmo;
    Vector2 moveDirection;
    Rigidbody2D rb2d;
    Camera mainCamera;
    Material defaultMaterial;
    Material damageMaterial;
    SpriteRenderer spriteRenderer;
    bool isReloading = false;
    bool isDead = false;


    void Awake()
    {
        currentAmmo = maxAmmo;
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultMaterial = spriteRenderer.material;
        damageMaterial = Resources.Load("DamageMaterial", typeof(Material)) as Material;
    }

    void Update()
    {
        if (isDead) return;

        timeSinceLastShot += Time.deltaTime;

        moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (Input.GetKey(KeyCode.Mouse0) && !isReloading && timeSinceLastShot >= timeBetweenShots)
        {
            timeSinceLastShot = 0f;
            if (currentAmmo > 0) ShootBullet();
            else SoundManager.instance.PlayNoAmmoSound();
        }

        if (Input.GetKeyDown(KeyCode.R) && !isReloading && currentAmmo < maxAmmo)
        {
            animator.Play("Reload");
            isReloading = true;
            SoundManager.instance.PlayReloadSound();
            Invoke(nameof(StopReloading), reloadTime);
        }

        if (!isReloading) animator.Play("Idle");
    }

    void StopReloading()
    {
        currentAmmo = maxAmmo;
        isReloading = false;
    }

    void ShootBullet()
    {
        currentAmmo--;
        SoundManager.instance.PlayShootSound();
        var bulletObject = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        var bullet = bulletObject.GetComponent<Bullet>();
        var lookDirection = GetLookDirection();
        bullet.Shoot(lookDirection, bulletSpeed);
    }

    void FixedUpdate()
    {
        if (isDead) return;
        rb2d.velocity = moveDirection.normalized * speed;
        LookTowardsMouse();
    }

    void LookTowardsMouse()
    {
        var lookDirection = GetLookDirection();
        transform.right = lookDirection;
    }

    Vector3 GetLookDirection()
    {
        var mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        return -transform.position + mousePosition;
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        health -= amount;
        onHealthChange.Invoke(health);
        SoundManager.instance.PlayHitSound();
        spriteRenderer.material = damageMaterial;
        Invoke(nameof(ResetMaterial), damageFlashDuration);

        if (health <= 0)
        {
            isDead = true;
            animator.Play("Death");
            GetComponent<Collider2D>().enabled = false;
            rb2d.velocity = Vector2.zero;
            moveDirection = Vector2.zero;
            Destroy(gameObject, 0.5f);
        }
    }

    void ResetMaterial()
    {
        spriteRenderer.material = defaultMaterial;
    }
}
