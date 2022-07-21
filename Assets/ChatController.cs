using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Backend.Connections;
using Backend.Handlers;
using Newtonsoft.Json;
using Shared.Messages;
using UnityEngine;
using Views;
using Views.Login;

public class ChatController : MonoBehaviour {
    [SerializeField] private Settings settings;
    [SerializeField] private GameObject connectingText;
    [SerializeField] private LoginView loginView;
    [SerializeField] private ChatView chatView;
    [SerializeField] private ErrorModalView errorView;

    private Client _client;
    private DnsEndPoint _endPoint;
    private HandlerSet _handlers;

    private Connection _connection;

    private void Start() {
        _handlers = new HandlerSet {
            new ConnectionHandler(this),
            new UserStateHandler(this),
            new IncomeMessageHandler(this),
        };

        _client = new Client(settings.appIdentifier);

        _client.IncomingDataEvent += ClientOnIncomingDataEvent;

        _endPoint = new DnsEndPoint(settings.connectionHost, settings.connectionPort);

        _client.Start();

        loginView.LoginClickEvent += Connect;

        Begin();
    }

    private void ClientOnIncomingDataEvent(Connection connection, byte[] bytes) {
        var message = ChatSerializer.Deserialize(bytes);
        _handlers.Handle(message);
    }

    private async void Begin() {
        connectingText.SetActive(true);

        string[] colors;
        try {
            var staticData = await GetColors();
            colors = staticData.Colors;
        } catch (Exception e) {
            Debug.LogError(e);
            ShowError("Cannot get static. Try later.");
            return;
        }

        connectingText.SetActive(false);

        ShowLogin(colors);
    }

    private void ShowLogin(string[] colors) {
        loginView.gameObject.SetActive(true);
        loginView.SetColors(colors);
    }

    private void HideLogin() {
        loginView.gameObject.SetActive(false);
    }

    public void ShowError(string text) {
        errorView.gameObject.SetActive(true);
        errorView.Set(text, () => {
            errorView.gameObject.SetActive(false);
        });
    }

    private async Task<StaticData> GetColors() {
        var client = new HttpClient {
            Timeout = TimeSpan.FromSeconds(2)
        };
        var response = await client.GetAsync($"{settings.apiUri}/api/v1/static");
        var content = await response.Content.ReadAsStringAsync();
        var staticData = JsonConvert.DeserializeObject<StaticData>(content);
        return staticData;
    }

    private async void Connect(string username, string color) {
        loginView.UnsetError();
        loginView.SetLoading(true);

        _connection = await _client.TryConnectAsync(_endPoint);

        loginView.SetLoading(false);

        if (_connection == null) {
            loginView.SetError("Cannot connect to server. Try again later.");
            return;
        }

        var request = new ConnectRequest {Name = username, Color = color};

        _connection.Send(request);
    }

    private void Update() {
        _client?.HandleIncomingMessages();
    }

    public void InitChat(UserStateNotify[] users, ChatMessage[] messages) {
        HideLogin();

        chatView.gameObject.SetActive(true);
        chatView.Init(this, users, messages);
    }

    public void UpdateUserState(ChatUser user, bool isOnline) {
        chatView.UpdateUserState(user, isOnline);
    }

    public void SendChatMessage(string text) {
        var message = new SendMessageRequest {Text = text};
        _connection.Send(message);
    }

    public void AddMessage(ChatMessage message) {
        chatView.AddMessage(message);
    }
}