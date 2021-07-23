using UnityEngine;

public class Bullet : MonoBehaviour
{
    public void Shoot(Vector2 direction, float speed)
    {
        var rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = direction.normalized * speed;
        transform.up = direction;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player")) return;
        var damageable = collider.GetComponent<IDamageable>();
        if (damageable != null) damageable.TakeDamage(1);
        Destroy(gameObject);
    }
}
