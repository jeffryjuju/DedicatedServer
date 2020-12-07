using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSend : MonoBehaviour
{
    /// <summary>
    /// To send data using TCP.
    /// </summary>
    /// <param name="_packet">The packet to send to the server.</param>
    private static void SendTcpData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.tcp.SendData(_packet);
    }

    /// <summary>
    /// To send data using UDP.
    /// </summary>
    /// <param name="_packet">The packet to send to the server.</param>
    private static void SendUdpData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.udp.SendData(_packet);
    }

    #region Packets
    /// <summary>
    /// Lets the server know that the welcome message was received.
    /// </summary>
    public static void WelcomeReceived()
    {
        using (Packet _packet = new Packet((int)ClientPackets.welcomeReceived))
        {
            _packet.Write(Client.instance.myId);
            _packet.Write(UIManager.instance.usernameField.text);

            SendTcpData(_packet);
        }
    }

    /// <summary>
    /// Sends player input to the server.
    /// </summary>
    /// <param name="_inputs"></param>
    public static void PlayerMovement(bool[] _inputs)
    {
        using (Packet _packet = new Packet((int)ClientPackets.playerMovement))
        {
            _packet.Write(_inputs.Length);
            foreach (bool _input in _inputs)
            {
                _packet.Write(_input);
            }

            _packet.Write(GameManager.players[Client.instance.myId].transform.position);

            SendUdpData(_packet);
        }
    }
    #endregion
}
