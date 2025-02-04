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

        public void Initialize()
        {
            vehicleCore.Initialize();
        }
    }
}