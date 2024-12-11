using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public interface IEffectAttachable
    {
        public Transform EffectRoot { get; }
    }
}
