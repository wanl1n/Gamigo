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

        private bool _monsterDead = false;
        public void SpawnMonster(MonsterData monsterData)
        {
            CurrentMonsterData = monsterData;
            CurrentMonsterData.MonsterDamaged += UpdateHealthBar;
            CurrentMonsterData.MonsterDeath += OnMonsterDied;
            _monsterDead = false;

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
            if (_monsterDead) return;

            float damage = Random.Range(10.0f, 20.0f);
            CurrentMonsterData.TakeDamage(damage);
            ClientPacketsHandler.SendDamageMonsterRequest(CurrentMonsterData.MonsterId, damage);
        }

        public void OnMonsterDied()
        {
            CurrentMonsterData.MonsterDamaged -= UpdateHealthBar;
            CurrentMonsterData.MonsterDeath -= OnMonsterDied;
            _monsterDead = true;
        }
    }
}
