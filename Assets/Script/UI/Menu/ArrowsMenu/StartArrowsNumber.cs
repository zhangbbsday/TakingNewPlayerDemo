using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace GameEditor
{
    public class StartArrowsNumber : UIElementBase
    {
        private InputField AttackArrowCount { get; set; }
        private InputField ReturnArrowCount { get; set; }

        protected override void Start()
        {
            base.Start();
            CanChangePosition = false;

            SetInputField();
        }

        private void SetInputField()
        {
            AttackArrowCount = Transform.Find("AttackArrowArea").Find("Input").GetComponent<InputField>();
            ReturnArrowCount = Transform.Find("ReturnArrowArea").Find("Input").GetComponent<InputField>();

            AttackArrowCount.onValueChanged.AddListener((value) => AttackArrowValueChange(value));
            ReturnArrowCount.onValueChanged.AddListener((value) => ReturnArrowValueChange(value));
        }

        private void AttackArrowValueChange(string value)
        {
            int.TryParse(value, out int res);
            GameManager.Instance.ArrowsManager.ChangeArrowCount(Arrow.ArrowType.AttackArrow, res);
        }

        private void ReturnArrowValueChange(string value)
        {
            int.TryParse(value, out int res);
            GameManager.Instance.ArrowsManager.ChangeArrowCount(Arrow.ArrowType.ReturnArrow, res);
        }
    }
}