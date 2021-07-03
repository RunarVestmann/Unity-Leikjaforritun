using UnityEngine;
using TMPro;

public class ValueUI : MonoBehaviour
{
    public string textBeforeValue;
    public bool changesColor;
    TextMeshProUGUI textMesh;
    void Awake()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }
    public void UpdateValue(int value)
    {
        textMesh.text = textBeforeValue + value;
        if (!changesColor) return;
        if (value == 2)
            textMesh.color = Color.yellow;
        else if (value == 1)
            textMesh.color = Color.red;
        else if (value <= 0)
            textMesh.color = Color.white;
    }
}
