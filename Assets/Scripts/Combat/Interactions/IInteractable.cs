using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public interface IInteractable
    {
        public void Interact(CharacterUnit characterUnit);        
    }
}
