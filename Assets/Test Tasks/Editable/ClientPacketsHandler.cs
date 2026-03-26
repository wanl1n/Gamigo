using System;
using System.Collections.Generic;
using TestTask.NonEditable;
using UnityEngine;

namespace TestTask.Editable
{
    public static class ClientPacketsHandler
    {
        #region Packet Handlers
        public static void LoginDataReceived(Packet packet)
        {
            int responseCode = packet.ReadInt();
            int clientId = packet.ReadInt();

            ClientManager.Instance.SetClientLogInStatus(responseCode, clientId);
        }

        public static void MonsterDataReceived(Packet packet)
        {
            int monsterId = packet.ReadInt();
            int monsterType = packet.ReadInt();
            string monsterName = packet.ReadString();
            float monsterMaxHealth = packet.ReadFloat();
            float monsterCurrentHealth = packet.ReadFloat();
            int clientId = packet.ReadInt();

            ClientManager.Instance.ClientMobsManager.
                SpawnMonster(new MonsterData(monsterId, (MonsterNames)monsterType, monsterMaxHealth, monsterCurrentHealth));
        }
        #endregion

        #region Packet Senders
        public static void SendLoginRequest()
        {
            Packet packet = new Packet(1);
            ClientManager.Instance.PacketSenderClient.SendToServer(packet);
        }
        #endregion
    }
}
