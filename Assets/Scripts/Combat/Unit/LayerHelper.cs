using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class LayerHelper
    {
        private static readonly string CharacterLayerName = "Character";
        private static readonly string EnemyLayerName = "Enemy";

        public static LayerMask CharacterLayerMask => 1 << LayerMask.NameToLayer(CharacterLayerName);
        public static LayerMask EnemyLayerMask => 1 << LayerMask.NameToLayer(EnemyLayerName);
    }
}