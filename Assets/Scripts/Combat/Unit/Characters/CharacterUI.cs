using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Combat
{
    public class CharacterUI : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text interactionText;

        public void SetInteractionState(bool active)
        {
            interactionText.gameObject.SetActive(active);
        }   
    }
}
