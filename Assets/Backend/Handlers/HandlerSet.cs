using System;
using System.Collections;
using System.Collections.Generic;
using Shared.Messages;

namespace Backend.Handlers {

public class HandlerSet : IEnumerable {
    private readonly Dictionary<Type, IHandler> _handlers = new();

    public void Add<T>(Handler<T> handler) where T : IMessage {
        _handlers.Add(typeof(T), handler);
    }

    public void Handle<T>(T message) where T : IMessage {
        if (_handlers.TryGetValue(message.GetType(), out var handler)) {
            handler.Handle(message);
        } else {
            throw new Exception($"Handler for {message.GetType()} not found");
        }
    }

    public IEnumerator GetEnumerator() {
        return _handlers.GetEnumerator();
    }
}

}