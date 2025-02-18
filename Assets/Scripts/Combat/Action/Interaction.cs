using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat.Actions
{
    public class Interaction : ActionBase
    {
        private List<IInteractable> _overlapedInteractableList;
        private List<IProgressiveInteractable> _overlapedProgressiveInteractableList;

        public Interaction()
        {
            _overlapedInteractableList = new();
            _overlapedProgressiveInteractableList = new();
        }

        public bool CanInteract()
        {
            return (_overlapedInteractableList.Count + _overlapedProgressiveInteractableList.Count) > 0;
        }

        public void OverlapColliders(CharacterUnit actor)
        {
            _overlapedInteractableList.Clear();
            _overlapedProgressiveInteractableList.Clear();

            var colliders = Physics2D.OverlapCircleAll(actor.GetPosition(), 16f);
            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent(out IInteractable interactable))
                {
                    _overlapedInteractableList.Add(interactable);
                }

                if (collider.TryGetComponent(out IProgressiveInteractable progressiveInteractable))
                {
                    _overlapedProgressiveInteractableList.Add(progressiveInteractable);
                }
            }
        }

        public override void Execute(CharacterUnit actor)
        {
            foreach (var interactable in _overlapedInteractableList)
            {
                interactable.Interact(actor);
            }
        }
        
        public void Progress(CharacterUnit actor)
        {
            foreach (var interactable in _overlapedProgressiveInteractableList)
            {
                interactable.InteractProgress(actor);
            }
        }

        public void InteractionEnd(CharacterUnit actor)
        {
            foreach (var interactable in _overlapedProgressiveInteractableList)
            {
                interactable.InteractEnd(actor);
            }
        }
    }
}