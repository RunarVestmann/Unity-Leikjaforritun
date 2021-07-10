using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Collider2D colliderToIgnore;
    public string[] tagsToIgnore;
    public int damageAmount;
    public float speed;
    [SerializeField] Vector2 direction;

    void Awake()
    {
        // Movement
        var rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = direction * speed;

        // Set up destruction when off screen
        var spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (spriteRenderer)
        {
            var script = spriteRenderer.gameObject.AddComponent<DestroyWhenInvisible>();
            script.objectToDestroy = gameObject;
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (ShouldAvoidCollision(collider)) return;
        var damagable = collider.GetComponent<IDamagable>();
        if (damagable != null)
        {
            damagable.TakeDamage(damageAmount);
            Destroy(gameObject);
        }
    }

    bool ShouldAvoidCollision(Collider2D collider)
    {
        if (collider == colliderToIgnore) return true;
        for (int i = 0; i < tagsToIgnore.Length; i++)
            if (tagsToIgnore[i] == collider.tag) return true;
        return false;
    }
}
