using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerSend
{
    #region TcpSendData
    /// <summary>
    /// Send a packet to a client via TCP.
    /// </summary>
    /// <param name="_toClient">The client to send the packet the packet to.</param>
    /// <param name="_packet">The packet to send to the client.</param>
    private static void SendTcpData(int _toClient, Packet _packet)
    {
        _packet.WriteLength();
        Server.clients[_toClient].tcp.SendData(_packet);
    }

    /// <summary>
    /// Send a packet to all clients via TCP.
    /// </summary>
    /// <param name="_packet">The packet to send.</param>
    private static void SendTcpDataToAll(Packet _packet)
    {
        _packet.WriteLength();
        for (int i = 1; i <= Server.MaxPlayer; i++)
        {
            Server.clients[i].tcp.SendData(_packet);
        }
    }

    /// <summary>
    /// Sends a packet to all clients except one via TCP.
    /// </summary>
    /// <param name="_exceptClient">The client to NOT send the data to.</param>
    /// <param name="_packet">The packet to send.</param>
    private static void SendTcpDataToAll(int _exceptClient, Packet _packet)
    {
        _packet.WriteLength();
        for (int i = 1; i <= Server.MaxPlayer; i++)
        {
            if (i != _exceptClient)
            {
                Server.clients[i].tcp.SendData(_packet);
            }
        }
    }
    #endregion

    #region UdpSendData
    /// <summary>
    /// Sends a packet to a client via UDP.
    /// </summary>
    /// <param name="_toClient">The client to send the packet the packet to.</param>
    /// <param name="_packet">The packet to send to the client.</param>
    private static void SendUdpData(int _toClient, Packet _packet)
    {
        _packet.WriteLength();
        Server.clients[_toClient].udp.SendData(_packet);
    }

    /// <summary>
    /// Sends a packet to all clients via UDP.
    /// </summary>
    /// <param name="_packet">The packet to send.</param>
    private static void SendUdpDataToAll(Packet _packet)
    {
        _packet.WriteLength();
        for (int i = 1; i <= Server.MaxPlayer; i++)
        {
            Server.clients[i].udp.SendData(_packet);
        }
    }

    /// <summary>
    /// Sends a packet to all clients except one via UDP.
    /// </summary>
    /// <param name="_exceptClient">The client to NOT send the data to.</param>
    /// <param name="_packet">The packet to send.</param>
    private static void SendUdpDataToAll(int _exceptClient, Packet _packet)
    {
        _packet.WriteLength();
        for (int i = 1; i <= Server.MaxPlayer; i++)
        {
            if (i != _exceptClient)
            {
                Server.clients[i].udp.SendData(_packet);
            }
        }
    }
    #endregion

    #region Packets
    /// <summary>
    /// Sends a welcome message to the given client.
    /// </summary>
    /// <param name="_toClient">The client to send the packet to.</param>
    /// <param name="_message">The message to send.</param>
    public static void Welcome(int _toClient, string _message)
    {
        using (Packet _packet = new Packet((int)ServerPackets.welcome))
        {
            _packet.Write(_message);
            _packet.Write(_toClient);

            SendTcpData(_toClient, _packet);
        }
    }

    /// <summary>
    /// Tells a client to spawn a player.
    /// </summary>
    /// <param name="_toClient">The client that should spawn the player.</param>
    /// <param name="_player">The player to spawn.</param>
    public static void SpawnPlayer(int _toClient, Player _player)
    {
        using (Packet _packet = new Packet((int)ServerPackets.spawnPlayer))
        {
            _packet.Write(_player.id);
            _packet.Write(_player.username);
            _packet.Write(_player.transform.position);

            SendTcpData(_toClient, _packet);
        }
    }

    /// <summary>
    /// Sends a player's updated position to all clients.
    /// </summary>
    /// <param name="_player">The player whose position to update.</param>
    public static void PlayerPosition(Player _player)
    {
        using (Packet _packet = new Packet((int)ServerPackets.playerPosition))
        {
            _packet.Write(_player.id);
            _packet.Write(_player.transform.position);

            SendUdpDataToAll(_packet);
        }
    }
    #endregion
}
