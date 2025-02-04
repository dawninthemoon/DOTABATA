using System.Collections;
using System.Collections.Generic;
using Game.StaticData;
using UnityEngine;

namespace Combat
{
    public class AIData
    {
        public float agentRadius;

        public ITargetable detectedTarget;
        public ITargetable selectedTarget;
        public float attackRange;
        public float detectRange;
        public EnemyTargetType targetType;
        
        public List<Collider2D> obstacles;
    }
}