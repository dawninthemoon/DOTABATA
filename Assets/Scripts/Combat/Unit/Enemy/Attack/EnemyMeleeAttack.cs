using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Game.Utils;
using UnityEngine;

namespace Combat
{
    public class EnemyMeleeAttack : IEnemyAttack
    {
        private bool _isProcessing;

        public void RequestAttack(EnemyUnit attacker, CombatManager combatManager)
        {
            Vector2 center = attacker.GetPosition() + attacker.Direction * 40;
            float angle = Mathf.Atan2(attacker.Direction.y, attacker.Direction.x) * Mathf.Rad2Deg + 90f;

            Vector2 rectSize = new Vector2(50f, 16f);
            var targets = Physics2D.OverlapBoxAll(center, rectSize, angle, LayerHelper.CharacterLayerMask);
            GizmoHelper.Instance.drawRectangle(center, rectSize * 0.5f, Color.red, 0.2f, angle);

            foreach (var target in targets.ToArray())
            {
                if (target.TryGetComponent<UnitBase>(out UnitBase unit))
                {
                    if (unit.CanTarget())
                    {
                        unit.ReceiveDamage(attacker.Damage, attacker: attacker);
                    }
                }
            }
        }

    #if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (_isProcessing)
            {

            }
        }
    #endif
    }
}