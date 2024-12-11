using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class VehicleTest : MonoBehaviour
    {
        [SerializeField]
        private Vector2 vehicleArea;   
        [SerializeField]
        private Vector2 offset;

        public Vector2 GetRandomArea()
        {
            Vector2 center = (Vector2)transform.position + offset;

            float randX = Random.Range(-vehicleArea.x * 0.5f, vehicleArea.x * 0.5f);
            float randY = Random.Range(-vehicleArea.y * 0.5f, vehicleArea.y * 0.5f);

            return center + new Vector2(randX, randY);
        }

    #if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Color deafultColor = Gizmos.color;
            
            Gizmos.color = Color.green;

            Vector2 center = (Vector2)transform.position + offset;

            Vector2 p00 = new Vector2(center.x - vehicleArea.x * 0.5f, center.y + vehicleArea.y * 0.5f);
            Vector2 p01 = new Vector2(center.x + vehicleArea.x * 0.5f, center.y + vehicleArea.y * 0.5f);
            Vector2 p10 = new Vector2(center.x - vehicleArea.x * 0.5f, center.y - vehicleArea.y * 0.5f);
            Vector2 p11 = new Vector2(center.x + vehicleArea.x * 0.5f, center.y - vehicleArea.y * 0.5f);

            Gizmos.DrawLine(p00, p01);
            Gizmos.DrawLine(p01, p11);
            Gizmos.DrawLine(p11, p10);
            Gizmos.DrawLine(p10, p00);

            Gizmos.color = deafultColor;
        }
    #endif
    }
}