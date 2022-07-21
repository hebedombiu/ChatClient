using MessagePack;

namespace Shared.Messages {

public abstract class Response : IMessage {
    [Key(0)] public int Parent { get; set; }
}

}