using System;
using System.Collections.Generic;
using Shared.Messages;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Views {

public class ChatView : MonoBehaviour {
    [SerializeField] private Transform usersRoot;
    [SerializeField] private TMP_Text userPrefab;
    [SerializeField] private Transform messagesRoot;
    [SerializeField] private TMP_Text messagePrefab;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Button sendButton;

    private readonly Dictionary<string, TMP_Text> _id2Text = new();
    private ChatController _chat;

    private void Start() {
        inputField.onSubmit.AddListener(SendChatMessage);
        sendButton.onClick.AddListener(SendChatMessage);
    }

    private void SendChatMessage() {
        _chat.SendChatMessage(inputField.text);
    }

    private void SendChatMessage(string _) {
        _chat.SendChatMessage(inputField.text);
    }

    public void Init(ChatController chat, UserStateNotify[] users, ChatMessage[] messages) {
        _chat = chat;
        foreach (var user in users) {
            AddUser(user.User, user.IsOnline);
        }
        foreach (var message in messages) {
            AddMessage(message);
        }
    }

    public void UpdateUserState(ChatUser user, bool isOnline) {
        if (_id2Text.TryGetValue(user.UserId, out var userView)) {
            userView.fontStyle = isOnline ? FontStyles.Bold : FontStyles.Normal;
        } else {
            AddUser(user, isOnline);
        }
    }

    private void AddUser(ChatUser user, bool isOnline) {
        var userView = Instantiate(userPrefab, usersRoot);
        userView.text = user.Name;
        ColorUtility.TryParseHtmlString(user.Color, out var color);
        userView.color = color;

        userView.fontStyle = isOnline ? FontStyles.Bold : FontStyles.Normal;

        _id2Text.Add(user.UserId, userView);
    }

    public void AddMessage(ChatMessage message) {
        var messageView = Instantiate(messagePrefab, messagesRoot);
        messageView.text = $"[{message.Name}] {message.Text}";
        ColorUtility.TryParseHtmlString(message.Color, out var color);
        messageView.color = color;
    }
}

}