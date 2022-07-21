using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ErrorModalView : MonoBehaviour {
    [SerializeField] private TMP_Text modalText;
    [SerializeField] private Button modalButton;

    public void Set(string text, UnityAction onClick) {
        modalText.text = text;
        modalButton.onClick.RemoveAllListeners();
        modalButton.onClick.AddListener(onClick);
    }
}