using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class ProjectileManager : MonoBehaviour
    {
        [SerializeField]
        private BulletTest testBulletPrefab;

        private void Start()
        {
            testBulletPrefab.gameObject.SetActive(false);
        }

        public BulletTest CreateProjectile(Vector3 pos)
        {
            var bullet = Instantiate(testBulletPrefab);
            bullet.transform.position = pos;

            return bullet;
        }
    }
}
