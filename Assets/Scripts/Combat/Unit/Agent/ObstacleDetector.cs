using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Combat
{
    public class ObstacleDetector : MonoBehaviour 
    {
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private Collider2D colliderSelf = null;
        
        public void Detect(AIData aiData) 
        {
            var colliders = Physics2D.OverlapCircleAll(transform.position, aiData.detectRange, layerMask).ToList();
            if (colliderSelf != null && colliders.Contains(colliderSelf))
            {
                colliders.Remove(colliderSelf);
            }
            aiData.obstacles = colliders;
        }
    }
}