using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat.Actions
{
    public class YuriActiveSkill : ActiveSkillAction
    {
        public override void Execute(CharacterUnit actor)
        {
            base.Execute(actor);
            Debug.Log($"[Execute] {GetType().Name}");   
        }
    }
}