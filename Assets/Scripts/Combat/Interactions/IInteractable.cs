using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public interface IInteractable
    {
        public void Interact(CharacterUnit characterUnit);        
    }

    public interface IProgressiveInteractable
    {
        public void InteractProgress(CharacterUnit characterUnit);
        public void InteractEnd(CharacterUnit characterUnit);
    }
}
