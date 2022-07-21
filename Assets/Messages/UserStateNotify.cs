using MessagePack;

namespace Shared.Messages {

[MessagePackObject]
public class UserStateNotify : IMessage {
    [Key(0)] public ChatUser User { get; set; }
    [Key(1)] public bool IsOnline { get; set; }
}

}