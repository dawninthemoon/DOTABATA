using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat.Actions
{
    public class Interaction : ActionBase
    {
        public override void Execute(CharacterUnit actor)
        {
            var colliders = Physics2D.OverlapCircleAll(actor.GetPosition(), 16f);
            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent(out IInteractable interactable))
                {
                    interactable.Interact(actor);
                }
            }
        }
        
        public void Progress(CharacterUnit actor)
        {
            var colliders = Physics2D.OverlapCircleAll(actor.GetPosition(), 16f);
            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent(out IProgressiveInteractable interactable))
                {
                    interactable.InteractProgress(actor);
                }
            }
        }

        public void InteractionEnd(CharacterUnit actor)
        {
            var colliders = Physics2D.OverlapCircleAll(actor.GetPosition(), 16f);
            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent(out IProgressiveInteractable interactable))
                {
                    interactable.InteractEnd(actor);
                }
            }
        }
    }
}