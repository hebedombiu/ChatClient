using System;
using System.Net;
using System.Threading.Tasks;
using Lidgren.Network;
using UnityEngine;

namespace Backend.Connections {

public class Client {
    private readonly NetClient _client;

    private Connection _connection;

    public event Action<Connection> ConnectedEvent;
    public event Action<Connection> DisconnectedEvent;
    public event Action<Connection, byte[]> IncomingDataEvent;

    public Client(string appIdentifier) {
        var configuration = new NetPeerConfiguration(appIdentifier);
        _client = new NetClient(configuration);
        Debug.Log($"[Client] Init {appIdentifier}");
    }

    public void Start() {
        _client.Start();
    }

    public async Task<Connection> TryConnectAsync(DnsEndPoint endPoint) {
        Debug.Log($"[Client] Connecting to {endPoint}");

        try {
            var addresses = await Dns.GetHostAddressesAsync($"{endPoint.Host}");
            var ipEndPoint = new IPEndPoint(addresses[0], endPoint.Port);

            Debug.Log($"[Client] address {ipEndPoint}");

            var connection = _client.Connect(ipEndPoint);

            while (
                connection.Status != NetConnectionStatus.Connected &&
                connection.Status != NetConnectionStatus.Disconnected
            ) {
                await Task.Yield();
            }

            return connection.Status == NetConnectionStatus.Disconnected ? null : new Connection(connection);
        } catch {
            return null;
        }
    }

    public void HandleIncomingMessages() {
        while (_client.ReadMessage(out var message)) {
            HandleIncomingMessage(message);
            _client.Recycle(message);
        }
    }

    private void HandleIncomingMessage(NetIncomingMessage message) {
        switch (message.MessageType) {
            // case NetIncomingMessageType.DebugMessage:
            //     Debug.Log(message.ReadString());
            //     break;
            case NetIncomingMessageType.StatusChanged:
                HandleStatusChanged(message);
                break;
            case NetIncomingMessageType.Data:
                HandleData(message);
                break;
        }
    }

    private void HandleStatusChanged(NetIncomingMessage message) {
        var status = (NetConnectionStatus) message.ReadByte();
        var reason = message.ReadString();

        switch (status) {
            case NetConnectionStatus.Connected:
                HandleConnected(message);
                break;
            case NetConnectionStatus.Disconnected:
                HandleDisconnected(message);
                break;
        }
    }

    private void HandleData(NetIncomingMessage message) {
        var length = message.ReadInt32();
        var bytes = message.ReadBytes(length);

        IncomingDataEvent?.Invoke(_connection, bytes);
    }

    private void HandleConnected(NetIncomingMessage message) {
        _connection = new Connection(message.SenderConnection);
        ConnectedEvent?.Invoke(_connection);
    }

    private void HandleDisconnected(NetIncomingMessage message) {
        DisconnectedEvent?.Invoke(_connection);
    }
}

}