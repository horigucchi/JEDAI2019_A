using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Horiguchi.Test
{
    public class HoriguchiTest : MonoBehaviour
    {
        [SerializeField]
        private Text label;
        [SerializeField]
        private Text label2;
        [SerializeField]
        private Slider slider;
        void Start()
        {

        }

        void Update()
        {
            if (label) label.text = YarnController.Instance.IsRolling ? "〇" : "×";
            if(label2) label2.text = YarnController.Instance.RollValue.ToString("f2");
            if(slider) slider.value = YarnController.Instance.RollingSpeed;
        }
    }
}
