using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class VehicleManager : MonoBehaviour
    {
        [SerializeField]
        private VehicleCore vehicleCore;
        public VehicleCore Core => vehicleCore;

        [SerializeField]
        private PlayerTurret playerTurret;

        private CombatManager _combatManager;

        public void Initialize()
        {
            vehicleCore.Initialize();
            playerTurret.Initialize();
        }

        public void SetDependency(CombatManager combatManager)
        {
            _combatManager = combatManager;
            playerTurret.SetDependency(combatManager);
        }
    }
}