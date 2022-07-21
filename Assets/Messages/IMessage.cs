using MessagePack;

namespace Shared.Messages {

[Union(0, typeof(ConnectRequest))]
[Union(1, typeof(ConnectResponse))]
[Union(2, typeof(SendMessageRequest))]
[Union(3, typeof(SendMessageResponse))]
[Union(4, typeof(UserStateNotify))]
[Union(5, typeof(IncomeMessageNotify))]
public interface IMessage { }

}