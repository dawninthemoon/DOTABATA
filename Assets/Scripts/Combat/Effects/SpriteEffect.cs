using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class SpriteEffect : EffectBase
    {
        [SerializeField]
        private Sprite spriteSequence;
        [SerializeField]
        private SpriteRenderer spriteRenderer;
        [SerializeField]
        private float animSpeed;

        private int _spriteIndex;

        public override void Init(System.Action onDestroy = null)
        {
            base.Init(_onDestroy);
            _spriteIndex = 0;
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();
            ProcessAnimation();
        }

        private void ProcessAnimation()
        {
            if (spriteSequence is null)
            {
                return;
            }

            
        }
    }
}