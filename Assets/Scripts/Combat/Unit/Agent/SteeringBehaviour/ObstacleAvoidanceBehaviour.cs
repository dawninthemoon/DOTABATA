using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class ObstacleAvoidanceBehaviour : SteeringBehaviour 
    {
        private float[] _dangersResultTemp;

        public override (float[] danger, float[] interest) GetSteering(float[] danger, float[] interest, AIData aiData) 
        {
            foreach (Collider2D obstacleCollider in aiData.obstacles) 
            {
                if (obstacleCollider.gameObject.Equals(gameObject)) 
                {
                    continue;
                }

                Vector2 directionToObstacle = obstacleCollider.ClosestPoint(transform.position) - (Vector2)transform.position;
                float distanceToObstacle = directionToObstacle.magnitude;

                float weight = (distanceToObstacle <= aiData.agentRadius) ? 1 : (aiData.detectRange - distanceToObstacle) / aiData.detectRange;

                Vector2 directionToObstacleNormalized = directionToObstacle.normalized;

                for (int i = 0; i < Constant.EightDirections.Length; ++i) 
                {
                    float result = Vector2.Dot(directionToObstacleNormalized, Constant.EightDirections[i]);
                    float valueToPutIn = result * weight;

                    if (valueToPutIn > danger[i]) 
                    {
                        danger[i] = valueToPutIn;
                    }
                }
            }
            _dangersResultTemp = danger;
            return (danger, interest);
        }
    }
}