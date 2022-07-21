using Lidgren.Network;
using Shared.Messages;
using UnityEngine;

namespace Backend.Connections {

public class Connection {
    private readonly NetConnection _connection;

    public Connection(NetConnection connection) {
        _connection = connection;
    }

    public bool IsConnected => _connection.Status == NetConnectionStatus.Connected;

    public void Send(IMessage message) {
        Send(ChatSerializer.Serialize(message));
    }

    private void Send(byte[] bytes) {
        var message = _connection.Peer.CreateMessage();

        message.Write((int) bytes.Length);
        message.Write(bytes);

        _connection.SendMessage(message, NetDeliveryMethod.ReliableOrdered, 0);
    }
}

}