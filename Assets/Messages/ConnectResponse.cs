using MessagePack;

namespace Shared.Messages {

[MessagePackObject]
public class ConnectResponse : Response {
    [Key(1)] public bool IsSuccess { get; set; }
    [Key(2)] public string Message { get; set; }
    [Key(3)] public UserStateNotify[] UserStates { get; set; }
    [Key(4)] public ChatMessage[] Messages { get; set; }
}

}