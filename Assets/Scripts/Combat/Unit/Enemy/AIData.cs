using System.Collections;
using System.Collections.Generic;
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
        
        public List<Collider2D> obstacles;
    }
}