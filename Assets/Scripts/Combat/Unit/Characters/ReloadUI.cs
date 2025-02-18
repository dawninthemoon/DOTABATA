using System.Collections;
using System.Collections.Generic;
using Game.Utils;
using UnityEngine;

namespace Combat
{
    public class ReloadUI : MonoBehaviour
    {
        [SerializeField]
        private Transform barTransform;

        [SerializeField]
        private float minLocalPosX;
        [SerializeField]
        private float maxLocalPosX;

        [SerializeField]
        private float yoyoTime;

        private float _currentLocalPosX;
        private float _timeAgo;

        public void SetEnable(bool enable)
        {
            gameObject.SetActive(enable);

            if (enable)
            {
                _currentLocalPosX = minLocalPosX;
                _timeAgo = 0f;
            }
        }

        private void Update()
        {
            float t = Mathf.Clamp01(_timeAgo / yoyoTime);

            float minX = t < 0.5f ? minLocalPosX : maxLocalPosX;
            float maxX = t < 0.5f ? maxLocalPosX : minLocalPosX;

            _currentLocalPosX = Mathf.Lerp(minX, maxX, Mathf.Repeat(t, 0.5f) * 2f);
            barTransform.localPosition = barTransform.localPosition.ChangeXPos(_currentLocalPosX);

            _timeAgo += Time.deltaTime;
            _timeAgo = Mathf.Repeat(_timeAgo, yoyoTime);
        }
    }
}
