using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HeartUI : MonoBehaviour
{
    List<Image> hearts = new List<Image>();

    void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
            hearts.Add(transform.GetChild(i).GetComponent<Image>());
    }

    public void ChangeHealth(int health)
    {
        for (int i = 0; i < hearts.Count; i++) hearts[i].enabled = i < health;
    }
}
