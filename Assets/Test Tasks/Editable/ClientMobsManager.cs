using System.Collections.Generic;
using TestTask.NonEditable;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TestTask.Editable
{
    public class ClientMobsManager : MonoBehaviour
    {
        [field: SerializeField] public TextMeshProUGUI MonsterName { get; private set; }
        [field: SerializeField] public Image MonsterPortrait { get; private set; }
        [field: SerializeField] public Image MonsterHealthBar { get; private set; }

        [field: SerializeField] public Sprite[] MonsterPortraits { get; private set; }

        public void SpawnMonster(MonsterData monsterData)
        {
            // Implement the logic to spawn a monster in the client game world using the provided monsterData.
            // This could involve instantiating a prefab, setting its properties based on monsterData, and adding it to the scene.
            MonsterPortrait.sprite = MonsterPortraits[(int)monsterData.MonsterType];
            MonsterName.text = monsterData.MonsterName;
            UpdateHealthBar(monsterData.MonsterCurrentHealth, monsterData.MonsterMaxHealth);
        }

        public void UpdateHealthBar(float currentHealth, float maxHealth)
        {
            // Implement the logic to update the health bar UI based on the current and maximum health of the monster.
            // This could involve setting the fill amount of a UI Image or updating a slider value.
            float healthPercentage = currentHealth / maxHealth;
            MonsterHealthBar.fillAmount = healthPercentage;
        }
    }
}
