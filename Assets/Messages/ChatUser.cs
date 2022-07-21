using MessagePack;

namespace Shared.Messages {

[MessagePackObject]
public class ChatUser {
    [Key(0)] public string UserId { get; set; }
    [Key(1)] public string Name { get; set; }
    [Key(2)] public string Color { get; set; }
}

}