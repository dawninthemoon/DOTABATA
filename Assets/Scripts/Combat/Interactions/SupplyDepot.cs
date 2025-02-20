using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Combat
{
    public class SupplyDepot : MonoBehaviour, IInteractable
    {
        /// <summary>
        /// <instanceID, cooldown>
        /// </summary>
        private Dictionary<int, float> _cooldownDictionary;
        private static readonly float CooldownSec = 15f;

        private void Awake()
        {
            _cooldownDictionary = new();
        }

        private void Update()
        {
            foreach (var pair in _cooldownDictionary.ToArray())
            {
                _cooldownDictionary[pair.Key] -= Time.deltaTime;
            }
        }

        public void Interact(CharacterUnit characterUnit)
        {
            if (!_cooldownDictionary.TryGetValue(characterUnit.InstanceID, out float cooldown))
            {
                cooldown = 0f;
                _cooldownDictionary.Add(characterUnit.InstanceID, cooldown);
            }

            if (cooldown <= 0f)
            {
                characterUnit.Weapon.ReloadImmediate();
                characterUnit.AddArmorPlate(1);

                _cooldownDictionary[characterUnit.InstanceID] = CooldownSec;
            }
        }
    }
}
