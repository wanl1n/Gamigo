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
            float monsterMaxHealth = packet.ReadFloat();
            float monsterCurrentHealth = packet.ReadFloat();

            ClientManager.Instance.ClientMobsManager.
                UpdateMonster(new MonsterData(monsterId, (MonsterNames)monsterType, monsterMaxHealth, monsterCurrentHealth));
        }
        #endregion

        #region Packet Senders
        public static void SendLoginRequest()
        {
            Packet packet = new Packet(1);
            ClientManager.Instance.PacketSenderClient.SendToServer(packet);
        }

        public static void SendDamageMonsterRequest(int monsterId, float damage)
        {
            using (Packet packet = new Packet(2))
            {
                packet.Write(monsterId);
                packet.Write(damage);

                ClientManager.Instance.PacketSenderClient.SendToServer(packet);
            }
        }
        #endregion
    }
}
