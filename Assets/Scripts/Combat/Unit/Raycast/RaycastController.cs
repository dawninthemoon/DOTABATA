using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class RaycastController : MonoBehaviour
    {
        [SerializeField]
        private LayerMask obstacleMask;

        private RaycastOrigins _raycastOrigins;
        private Vector2 _targetHalfSize;

        private float _skinWidth = 1f;

        public void Initialize(Vector2 targetHalfSize) 
        {
            _raycastOrigins = new();

            _targetHalfSize = targetHalfSize;
        }

        public Vector2 ProcessMovement(Vector3 position, Vector2 moveAmount)
        {
            UpdateRaycastOrigins(position);

            if (Mathf.Abs(moveAmount.x) > 0f)
            {
                HorizontalCollisions(ref moveAmount);
            }
            if (Mathf.Abs(moveAmount.y) > 0f)
            {
                VerticalCollisions(ref moveAmount);
            }

            return moveAmount;
        }
        
        public void UpdateRaycastOrigins(Vector3 position) 
        {
            _raycastOrigins.middleCenter = new Vector2(position.x, position.y + _targetHalfSize.y);
            _raycastOrigins.bottomCenter = new Vector2(position.x, position.y);

            _raycastOrigins.middleLeft = new Vector2(position.x - _targetHalfSize.x, position.y + _targetHalfSize.y - _skinWidth);
            _raycastOrigins.middleRight = new Vector2(position.x + _targetHalfSize.x, position.y + _targetHalfSize.y - _skinWidth);
        }

        private void HorizontalCollisions(ref Vector2 moveAmount) 
        {
            float directionX = Mathf.Sign(moveAmount.x);
            float rayLength = Mathf.Abs(moveAmount.x);

            Vector2 rayOrigin = (directionX < 0f) ? _raycastOrigins.middleLeft : _raycastOrigins.middleRight;

            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, obstacleMask);
            Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength, Color.green);
            if (hit.collider != null) 
            {
                moveAmount.x = hit.distance * directionX;
            }
        }

        private void VerticalCollisions(ref Vector2 moveAmount) 
        {
            float directionY = Mathf.Sign(moveAmount.y);
            float rayLength = Mathf.Abs(moveAmount.y);

            Vector2 rayOrigin = (directionY < 0f) ? _raycastOrigins.bottomCenter : _raycastOrigins.middleCenter;

            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, obstacleMask);
            Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, Color.green);
            if (hit.collider != null) 
            {
                moveAmount.y = hit.distance * directionY;
            }
        }
    }
}
