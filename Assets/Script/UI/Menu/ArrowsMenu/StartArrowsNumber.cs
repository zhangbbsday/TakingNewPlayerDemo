using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace GameEditor
{
    public class StartArrowsNumber : UIElementBase
    {
        public static StartArrowsNumber Instance { get; private set; }
        private InputField AttackArrowCount { get; set; }
        private InputField ReturnArrowCount { get; set; }

        protected override void Awake()
        {
            base.Awake();
            SetInputField();
            Instance = this;
        }

        protected override void Start()
        {
            base.Start();
            CanChangePosition = false;

        }

        public void SetValues(int[] values)
        {
            AttackArrowCount.text = values[(int)Arrow.ArrowType.AttackArrow].ToString();
            ReturnArrowCount.text = values[(int)Arrow.ArrowType.ReturnArrow].ToString();
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