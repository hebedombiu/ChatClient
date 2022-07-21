using MessagePack;
using MessagePack.Resolvers;

namespace Shared.Messages {

public static class ChatSerializer {
    private static MessagePackSerializerOptions _options;

    public static MessagePackSerializerOptions Options {
        get {
            if (_options == null) {
                StaticCompositeResolver.Instance.Register(
                    GeneratedResolver.Instance,
                    NativeGuidResolver.Instance,
                    StandardResolver.Instance
                );

                _options = MessagePackSerializerOptions.Standard.WithResolver(StaticCompositeResolver.Instance);
            }

            return _options;
        }
    }

    public static byte[] Serialize(IMessage message) {
        return MessagePackSerializer.Serialize(message, Options);
    }

    public static string SerializeToJson(IMessage message) {
        return MessagePackSerializer.SerializeToJson(message, Options);
    }

    public static IMessage Deserialize(byte[] bytes) {
        return MessagePackSerializer.Deserialize<IMessage>(bytes, Options);
    }
}

}