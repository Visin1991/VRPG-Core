using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace V
{
    public class ManaBar : MonoBehaviour
    {

        Image manaImage;

        private void Start()
        {
            manaImage = GetComponent<Image>();
            GameUIPr.Instance.Adapter_Manabar_CA -= ValueChange;
            GameUIPr.Instance.Adapter_Manabar_CA += ValueChange;
        }

        public void ValueChange(float current, float max)
        {
            if (manaImage != null)
            {
                manaImage.fillAmount = current / max;
            }
        }
    }
}
