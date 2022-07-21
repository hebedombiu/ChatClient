using Newtonsoft.Json;
using Shared.Messages;
using UnityEngine;

namespace Backend.Handlers {

public class ConnectionHandler : Handler<ConnectResponse> {
    private readonly ChatController _chatController;

    public ConnectionHandler(
        ChatController chatController
    ) {
        _chatController = chatController;
    }

    protected override void Handle(ConnectResponse message) {
        Debug.Log(JsonConvert.SerializeObject(message, Formatting.Indented));

        if (!message.IsSuccess) {
            _chatController.ShowError("auth error");
            return;
        }

        _chatController.InitChat(message.UserStates, message.Messages);
    }
}

}