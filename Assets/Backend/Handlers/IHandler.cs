using Shared.Messages;

namespace Backend.Handlers {

public interface IHandler {
    public void Handle(IMessage message);
}

}