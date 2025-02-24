using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public abstract class InteractableObject : MonoBehaviour
    {
        [field: SerializeField]
        public GameObject InteractUIObj { get; private set; }
        private bool _canInteract;
        
        public virtual void SetInteractableFlag()
        {
            _canInteract = true;
        }

        protected virtual void LateUpdate()
        {
            InteractUIObj.SetActive(_canInteract);
            _canInteract = false;
        }

        public abstract void Interact(CharacterUnit characterUnit);        
    }
}
