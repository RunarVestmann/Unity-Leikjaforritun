using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [HideInInspector]
    public bool isGrounded = false;

    Collider2D[] results = new Collider2D[100];
    Collider2D myCollider;
    int groundLayer;

    void Awake()
    {
        myCollider = GetComponent<Collider2D>();
        groundLayer = 1 << LayerMask.NameToLayer("Ground");
    }

    void FixedUpdate()
    {
        int count = Physics2D.OverlapBoxNonAlloc(transform.position, myCollider.bounds.size, 0f, results, groundLayer);
        isGrounded = count > 0;
    }
}
