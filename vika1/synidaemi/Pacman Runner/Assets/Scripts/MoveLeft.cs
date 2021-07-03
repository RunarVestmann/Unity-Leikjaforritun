using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float speed;
    void Awake()
    {
        Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = Vector2.left * speed;
    }
}
