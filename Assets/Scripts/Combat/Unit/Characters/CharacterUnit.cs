using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class CharacterUnit : UnitBase
    {
        [SerializeField]
        private CharacterRenderer characterRenderer;
        [SerializeField]
        private BulletTest testBulletPrefab;

        private void Start()
        {
            testBulletPrefab.gameObject.SetActive(false);
        }

        private void Update()
        {
            Vector2 input;
            ControlInput();
            void ControlInput()
            {
                input.x = Input.GetAxisRaw("Horizontal");
                input.y = Input.GetAxisRaw("Vertical");
            }

            UpdatePosiiton();
            void UpdatePosiiton()
            {
                Vector3 moveVector = input.normalized * moveSpeed; 
                transform.position += moveVector * Time.deltaTime;
            }

            bool fired = Input.GetMouseButtonDown(0) && CanAttack();
            if (fired)
            {
                Fire();
            }
            characterRenderer.ProcessInput(input, fired);

            ProcessAttackWaitTimer();
        }
        private void Fire()
        {
            var bullet = Instantiate(testBulletPrefab);
            bullet.transform.position = characterRenderer.BulletTransform.position;

            Vector2 mousePosition = Game.Utils.ExMouse.GetMouseWorldPosition();
            Vector2 dir = (mousePosition - (Vector2)transform.position).normalized;

            bullet.Initialize(dir, 1000f);

            OnAttack();
        }
    }
}