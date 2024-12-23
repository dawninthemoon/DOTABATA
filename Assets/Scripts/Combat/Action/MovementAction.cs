using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat.Action
{
    public class MovementAction : ActionBase
    {
        private Vector2 _direction;

        public void SetDirection(Vector2 dir)
        {
            _direction = dir;
        }

        public override void Use(CharacterUnit actor)
        {
            Vector2 moveVector = _direction.normalized * actor.MoveSpeed; 
            actor.AddPosition(moveVector * Time.deltaTime);
        }
    }
}