using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.StaticData;

namespace Combat.Actions
{
    public class ActiveSkillAction : ActionBase
    {
        private StaticDataSkill _data;
        public StaticDataSkill Data => _data;

        public override void Execute(CharacterUnit actor)
        {
            
        }
    }
}