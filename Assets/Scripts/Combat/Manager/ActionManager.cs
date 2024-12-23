using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat.Action
{
    public class ActionManager 
    {
        private Dictionary<ActionType, ActionBase> _actionDictionary;

        public ActionBase GetActionInstance()
        {
            return null;
        }
    }
}