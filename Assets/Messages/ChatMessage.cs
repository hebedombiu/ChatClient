using MessagePack;

namespace Shared.Messages {

[MessagePackObject]
public class ChatMessage {
    [Key(0)] public string Name { get; set; }
    [Key(1)] public string Color { get; set; }
    [Key(2)] public string Text { get; set; }
    [Key(3)] public long Time { get; set; }
}

}