using System;
using LoadingIcon;
using Shared.Messages;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Views.Login {

public class LoginView : MonoBehaviour {
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private Button loginButton;
    [SerializeField] private TMP_Text loginButtonText;
    [SerializeField] private ToggleGroup colorsRoot;
    [SerializeField] private ToggleColorView colorTogglePrefab;
    [SerializeField] private LoadingIconView loginButtonIcon;
    [SerializeField] private TMP_Text errorText;

    public event Action<string, string> LoginClickEvent;

    private void Start() {
        loginButton.onClick.AddListener(() => {
            if (string.IsNullOrWhiteSpace(nameInput.text)) {
                SetError("Name cannot be empty");
                return;
            }

            var colorToggle = colorsRoot.GetFirstActiveToggle();
            var color = colorToggle.GetComponent<ToggleColorView>().Color;

            LoginClickEvent?.Invoke(nameInput.text, ColorUtility.ToHtmlStringRGB(color));
        });
    }

    public void SetError(string text) {
        errorText.text = text;
        errorText.gameObject.SetActive(true);
    }

    public void UnsetError() {
        errorText.gameObject.SetActive(false);
    }

    public void SetLoading(bool isLoading) {
        if (isLoading) {
            loginButton.enabled = false;
            loginButtonText.gameObject.SetActive(false);
            loginButtonIcon.gameObject.SetActive(true);
        } else {
            loginButton.enabled = true;
            loginButtonText.gameObject.SetActive(true);
            loginButtonIcon.gameObject.SetActive(false);
        }
    }

    public void SetColors(string[] colors) {
        foreach (var colorString in colors) {
            ColorUtility.TryParseHtmlString(colorString, out var color);
            var toggleView = Instantiate(colorTogglePrefab, colorsRoot.transform);
            toggleView.SetColor(color);
            toggleView.SetGroup(colorsRoot);
        }
    }
}

}