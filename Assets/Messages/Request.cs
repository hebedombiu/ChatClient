using MessagePack;

namespace Shared.Messages {

public abstract class Request : IMessage {
    [Key(0)] public int Id { get; set; }
}

}