using Newtonsoft.Json;
using Shared.Messages;
using UnityEngine;

namespace Backend.Handlers {

public class UserStateHandler : Handler<UserStateNotify> {
    private readonly ChatController _chatController;

    public UserStateHandler(
        ChatController chatController
    ) {
        _chatController = chatController;
    }

    protected override void Handle(UserStateNotify message) {
        Debug.Log(JsonConvert.SerializeObject(message, Formatting.Indented));
        _chatController.UpdateUserState(message.User, message.IsOnline);
    }
}

}