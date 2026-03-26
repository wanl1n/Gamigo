using System.Collections.Generic;
using TestTask.NonEditable;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TestTask.Editable
{
    public class ClientMobsManager : MonoBehaviour
    {
        // UI Elements for displaying monster information
        [field: SerializeField] public TextMeshProUGUI MonsterName { get; private set; }
        [field: SerializeField] public Image MonsterPortrait { get; private set; }
        [field: SerializeField] public Image MonsterHealthBar { get; private set; }

        // Array of monster portraits corresponding to different monster types
        [field: SerializeField] public Sprite[] MonsterPortraits { get; private set; }

        [field: SerializeField] public MonsterData CurrentMonsterData;

        public void UpdateMonster(MonsterData monsterData)
        {
            if (CurrentMonsterData != null)
                CurrentMonsterData.MonsterDamaged -= UpdateHealthBar;

            CurrentMonsterData = monsterData;
            CurrentMonsterData.MonsterDamaged += UpdateHealthBar;

            // UI
            MonsterPortrait.sprite = MonsterPortraits[(int)monsterData.MonsterType];
            MonsterName.text = monsterData.MonsterName;
            UpdateHealthBar(CurrentMonsterData.MonsterCurrentHealth/CurrentMonsterData.MonsterMaxHealth);
        }

        public void UpdateHealthBar(float healthPercentage)
        {
            MonsterHealthBar.fillAmount = healthPercentage;
        }

        public void DamageMonster()
        {
            float damage = Random.Range(10.0f, 20.0f);
            CurrentMonsterData.TakeDamage(damage);
            ClientPacketsHandler.SendDamageMonsterRequest(CurrentMonsterData.MonsterId, damage);
        }
    }
}
