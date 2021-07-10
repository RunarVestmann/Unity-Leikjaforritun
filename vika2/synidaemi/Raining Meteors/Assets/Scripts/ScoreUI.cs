using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    float score = 0;
    bool canCount = true;
    TextMeshProUGUI textMesh;

    void Awake()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    public void StopCounting()
    {
        canCount = false;
    }

    void Update()
    {
        if (!canCount) return;
        score += Time.deltaTime;
        textMesh.text = score.ToString("N0");
    }
}
