using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Game.StaticData;

namespace Combat.Actions
{
    public class ActionManager 
    {
        private Dictionary<ActionType, ActionBase> _actionDictionary;

        public ActionManager()
        {
            Initialize();
        }

        public void Initialize()
        {
            _actionDictionary = new Dictionary<ActionType, ActionBase>()
            {
                { ActionType.DefaultMove, new MovementAction()},
            };
        }

        public ActionBase GetActionInstance(ActionType type)
        {
            return _actionDictionary.TryGetValue(type, out var instance) ? instance : null;
        }

        public ActionBase GetActionInstance(CharacterUnit unit, InputType inputType)
        {
            var actionData = StaticDataManager.Instance.GetCharacterActionData(unit.Data.keyIndex, inputType);
            if (actionData == null)
            {
                return null;
            }

            return GetActionInstance(actionData.actionType);
        }
    }
}