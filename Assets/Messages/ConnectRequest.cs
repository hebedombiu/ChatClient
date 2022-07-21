using MessagePack;

namespace Shared.Messages {

[MessagePackObject]
public class ConnectRequest : Request {
    [Key(1)] public string Name { get; set; }
    [Key(2)] public string Color { get; set; }
}

}