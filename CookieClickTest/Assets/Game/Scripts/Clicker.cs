using UnityEngine;
using TMPro;

public class Clicker : MonoBehaviour
{
    public TextMeshProUGUI clickText;
    public float clickCount = 0;
    public float perClick = 1;

    public void OnClicked()
    {
        clickCount += perClick;
        UpdateText();
    }

    void UpdateText()
    {
        clickText.text = $"{Mathf.FloorToInt(clickCount)} cookies";
    }
}
