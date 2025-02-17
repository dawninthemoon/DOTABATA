using System.Collections;
using System.Collections.Generic;
using Game.Core;
using UnityEngine;

namespace Combat
{
    public class ProjectileManager : MonoBehaviour
    {
        private static readonly string ProjectilePathBase = "Projectiles/";

        private CombatManager _combatManager;

        public void SetDependency(CombatManager combatManager)
        {
            _combatManager = combatManager;
        }

        public BulletTest CreateProjectile(string name, Vector3 pos)
        {
            var prefab = AssetLoader.Instance.GetComponentObject<BulletTest>($"{ProjectilePathBase}{name}");
            var bullet = Instantiate(prefab);
            bullet.SetDependency(_combatManager);
            bullet.transform.position = pos;

            return bullet;
        }
    }
}
