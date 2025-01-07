using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Combat
{
    public class SeekBehaviour : SteeringBehaviour 
    {
        private Vector2 _targetPositionCached;
        private float[] _interestsTemp;

        public override (float[] danger, float[] interest) GetSteering(float[] danger, float[] interest, AIData aiData) 
        {
            if (aiData.detectedTarget != null) 
            {
                _targetPositionCached = aiData.detectedTarget.GetPosition();
            }
            else
            {
                return (danger, interest);
            }

            if (Vector2.Distance(transform.position, _targetPositionCached) < aiData.agentRadius * 0.5f) 
            {
                //aiData.detectedTarget = null;
                return (danger, interest);
            }

            Vector2 directionToTarget = (_targetPositionCached - (Vector2)transform.position);
            for (int i = 0; i < interest.Length; ++i) 
            {
                float result = Vector2.Dot(directionToTarget.normalized, Constant.EightDirections[i]);
                if (result > 0) 
                {
                    float valueToPutIn = result;
                    if (valueToPutIn > interest[i]) 
                    {
                        interest[i] = valueToPutIn;
                    }
                }
            }
            _interestsTemp = interest;
            return (danger, interest);
        }

        private void OnDrawGizmos() 
        {
            if (Application.isPlaying && _interestsTemp != null) 
            {
                if (_interestsTemp != null) 
                { 
                    Gizmos.color = Color.green;
                    for (int i = 0; i < _interestsTemp.Length; ++i) 
                    {
                        Gizmos.DrawRay(transform.position, 36f * Constant.EightDirections[i] * _interestsTemp[i]);
                    }
                }
            }
        }
    }
}