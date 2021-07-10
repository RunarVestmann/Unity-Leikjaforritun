using UnityEngine;

public class ScreenClamp : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    Vector2 screenBoundaries;

    float xOffset = 0f;
    float yOffset = 0f;

    void Awake()
    {
        screenBoundaries = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        if (spriteRenderer)
        {
            xOffset = spriteRenderer.bounds.size.x / 2f;
            yOffset = spriteRenderer.bounds.size.y / 2f;
        }
    }

    void LateUpdate()
    {
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -screenBoundaries.x + xOffset, screenBoundaries.x - xOffset);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, -screenBoundaries.y + yOffset, screenBoundaries.y - yOffset);
        transform.position = clampedPosition;
    }
}
