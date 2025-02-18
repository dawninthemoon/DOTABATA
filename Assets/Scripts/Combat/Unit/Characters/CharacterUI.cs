using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Combat
{
    public class CharacterUI : MonoBehaviour
    {
        [SerializeField]
        private ReloadUI reloadUI;
        [SerializeField]
        private TMP_Text interactionText;

        private void Start()
        {
            reloadUI.SetEnable(false);
        }

        public void OnReloadStart()
        {
            reloadUI.SetEnable(true);
        }

        public void OnReloadEnd()
        {
            reloadUI.SetEnable(false);
        }

        public void SetInteractionState(bool active)
        {
            interactionText.gameObject.SetActive(active);
        }   
    }
}
