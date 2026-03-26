using TestTask.NonEditable;
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
                SendMonsterData(ServerMock.Instance.ServerMobsManager.MonsterData);
        }

        public static void DamageMonsterRequest(Packet packet)
        {
            var monsterId = packet.ReadInt();
            var damage = packet.ReadFloat();

            ServerMock.Instance.ServerMobsManager.MonsterData.TakeDamage(damage);
            SendMonsterData(ServerMock.Instance.ServerMobsManager.MonsterData);
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
        public static void SendMonsterData(MonsterData monsterData)
        {
            using (Packet packet = new Packet(2))
            {
                packet.Write(monsterData.MonsterId);
                packet.Write((int)monsterData.MonsterType);
                packet.Write(monsterData.MonsterMaxHealth);
                packet.Write(monsterData.MonsterCurrentHealth);

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