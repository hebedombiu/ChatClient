using Shared.Messages;

namespace Backend.Handlers {

public abstract class Handler<T> : IHandler where T : IMessage {
    public void Handle(IMessage message) {
        Handle((T) message);
    }

    protected abstract void Handle(T message);
}

}