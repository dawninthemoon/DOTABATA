using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat.Actions
{
    public class MovementAction : ActionBase
    {
        private Vector2 _direction;

        public void SetDirection(Vector2 dir)
        {
            _direction = dir;
        }

        public override void Execute(CharacterUnit actor)
        {
            Vector2 dir = _direction.normalized;
            Vector2 moveVector = dir * actor.MoveSpeed * Time.deltaTime;
            actor.Move(moveVector);
        }
    }
}