using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class InteractiveArea : MonoBehaviour
    {
        private IInteractable _selected;
        public IInteractable Selected => _selected;

        public void Reset()
        {
            _selected = null;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IInteractable interactiveObj))
            {
                _selected = interactiveObj;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out IInteractable interactiveObj))
            {
                if (_selected == interactiveObj)
                {
                    _selected = null;
                }
            }
        }
    }
}