using MessagePack;

namespace Shared.Messages {

[MessagePackObject]
public class IncomeMessageNotify : IMessage {
    [Key(0)] public ChatMessage Message { get; set; }
}

}