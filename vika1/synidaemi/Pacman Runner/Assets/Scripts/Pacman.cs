using UnityEngine;
using UnityEngine.Events;

public class Pacman : MonoBehaviour
{
    public int health;
    public float speed;
    public float offset;
    public UnityEvent<int> onHealthChange;
    public UnityEvent<int> onScoreChange;
    Vector3[] positions = new Vector3[3];
    int currentPosition = 1;
    Vector3 target;
    int score = 0;

    void Awake()
    {
        // Top
        positions[0] = transform.position + Vector3.up * offset;

        // Middle
        positions[1] = transform.position;

        // Bottom
        positions[2] = transform.position + Vector3.down * offset;

        target = transform.position;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentPosition < positions.Length - 1)
            {
                currentPosition++;
                target = positions[currentPosition];
            }
        }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentPosition > 0)
            {
                currentPosition--;
                target = positions[currentPosition];
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (health <= 0) return;

        if (collider.CompareTag("Ghost"))
        {
            Destroy(collider.gameObject);
            health--;
            SoundManager.instance.PlayHitSound();
            if (health <= 0)
            {
                SoundManager.instance.PlayDeathSound();
                var animator = GetComponent<Animator>();
                animator.Play("Shrink");
                Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
            }
            onHealthChange.Invoke(health);
        }
        else if (collider.CompareTag("Dot"))
        {
            Destroy(collider.gameObject);
            score++;
            SoundManager.instance.PlayScoreSound();
            onScoreChange.Invoke(score);
        }
    }
}
