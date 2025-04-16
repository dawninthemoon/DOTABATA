using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.StaticData;

namespace Combat.Actions
{
    public interface IActiveSkill
    {
        public void Initialize(StaticDataSkill data);
        public StaticDataSkill Data { get; }
    }

    public class ActiveSkillAction : ActionBase, IActiveSkill
    {
        private StaticDataSkill _data;
        public StaticDataSkill Data => _data;

        public void Initialize(StaticDataSkill data)
        {
            _data = data;
        }

        public override void Execute(CharacterUnit actor)
        {
            
        }
    }
}