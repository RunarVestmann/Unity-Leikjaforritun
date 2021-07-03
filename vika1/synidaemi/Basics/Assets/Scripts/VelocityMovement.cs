using UnityEngine;

public class VelocityMovement : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb2d;

    Vector2 moveDirection;

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(x, y);
    }

    void FixedUpdate()
    {
        rb2d.velocity = moveDirection.normalized * speed;
    }
}
