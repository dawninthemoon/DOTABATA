using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Combat
{
    public class SupplyDepot : MonoBehaviour, IInteractable
    {
        public void Interact(CharacterUnit characterUnit)
        {
            characterUnit.Weapon.ReloadImmediate();
            characterUnit.AddArmorPlate(1);
        }
    }
}
