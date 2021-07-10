using UnityEngine;

public class Meteor : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;

    void Awake()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
    }

}
