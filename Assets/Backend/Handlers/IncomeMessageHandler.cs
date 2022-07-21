using Newtonsoft.Json;
using Shared.Messages;
using UnityEngine;

namespace Backend.Handlers {

public class IncomeMessageHandler : Handler<IncomeMessageNotify> {
    private readonly ChatController _chatController;

    public IncomeMessageHandler(
        ChatController chatController
    ) {
        _chatController = chatController;
    }

    protected override void Handle(IncomeMessageNotify message) {
        Debug.Log(JsonConvert.SerializeObject(message, Formatting.Indented));
        _chatController.AddMessage(message.Message);
    }
}

}