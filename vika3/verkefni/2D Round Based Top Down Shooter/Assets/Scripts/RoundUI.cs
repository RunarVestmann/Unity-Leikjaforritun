using UnityEngine;
using TMPro;

public class RoundUI : MonoBehaviour
{
    TextMeshProUGUI textMesh;

    void Awake()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    public void OnRoundChange(int round)
    {
        textMesh.text = $"Round: {round}";
    }
}
