using System.Collections.Generic;
using System.Linq;
using TestTask.NonEditable;
using Unity.VisualScripting;
using UnityEngine;

namespace TestTask.Editable
{
    public static class ServerPacketsHandler
    {
        #region Packet Handlers
        public static void LoginRequest(Packet packet)
        {
            var clientLogInResponse = ServerMock.Instance.TryConnectClient(out var clientId);
            SendLoginResponse(clientLogInResponse, clientId);

            if (clientLogInResponse == LoginResponse.Success)
                SendNewMonsterData();
        }

        public static void DamageMonsterRequest(Packet packet)
        {
            var monsterId = packet.ReadInt();
            var damage = packet.ReadFloat();

            ServerMock.Instance.ServerMobsManager.MonsterData.TakeDamage(damage);
        }

        public static void ColorListRequest(Packet packet)
        {
            SendColorsList();
        }

        #endregion

        #region Packet Senders
        public static void SendLoginResponse(LoginResponse response, int clientId)
        {
            using (Packet packet = new Packet(1))
            {
                packet.Write((int)response);
                packet.Write(clientId);

                ServerMock.Instance.PacketSenderServer.SendToClient(packet);
            }
        }

        public static void SendNewMonsterData()
        {
            using (Packet packet = new Packet(2))
            {
                MonsterData monsterData = ServerMock.Instance.ServerMobsManager.MonsterData;
                packet.Write(monsterData.MonsterId);
                packet.Write((int)monsterData.MonsterType);
                packet.Write(monsterData.MonsterMaxHealth);
                packet.Write(monsterData.MonsterCurrentHealth);

                ServerMock.Instance.PacketSenderServer.SendToClient(packet);
            }
        }

        public static void SendColorsList()
        {
            using (Packet packet = new Packet(3))
            {
                IEnumerable<Color> generatedColors = ServerMock.Instance.ServerColors.GetServerColors();
                packet.Write(generatedColors.Count());
                foreach (Color color in generatedColors)
                {
                    packet.Write(color.r);
                    packet.Write(color.g);
                    packet.Write(color.b);
                }

                ServerMock.Instance.PacketSenderServer.SendToClient(packet);
            }
        }
        #endregion
    }
}

public enum LoginResponse
{
    Success = 0,
    Failure = 1,
}