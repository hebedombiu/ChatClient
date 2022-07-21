using MessagePack;

namespace Shared.Messages {

[MessagePackObject]
public class SendMessageRequest : Request {
    [Key(1)] public string Text { get; set; }
}

}