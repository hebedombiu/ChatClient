using UnityEngine;
using UnityEngine.UI;

public class ToggleColorView : MonoBehaviour {
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Toggle toggle;

    public Color Color { get; private set; }

    public void SetColor(Color color) {
        Color = color;
        backgroundImage.color = Color;
    }

    public void SetGroup(ToggleGroup group) {
        toggle.group = group;
    }
}