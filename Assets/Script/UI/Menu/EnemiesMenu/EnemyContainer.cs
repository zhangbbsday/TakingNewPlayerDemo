using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace GameEditor
{
    public class EnemyContainer : UIElementBase
    {
        public int Id { get; private set; }
        private Text IdText { get; set; }
        private Dropdown EnemyTypeSelecter { get; set; }

        public void Init(int id, string[] options, EnemyContainerManager.EnemyType type)
        {
            Id = id;

            SetOptions(options, type);
        }

        public EnemyContainerManager.EnemyType GetEnemyType()
        {
            int value = EnemyTypeSelecter.value;
            return GameManager.Instance.EnemyContainerManager.GetEnemyTypeByValue(value);
        }

        protected override void Start()
        {
            base.Start();
            CanChangePosition = false;

            IdText = Transform.Find("Id").Find("Number").GetComponent<Text>();
            IdText.text = Id.ToString();
        }

        private void SetOptions(string[] options, EnemyContainerManager.EnemyType type)
        {
            Dropdown[] childs = GetComponentsInChildren<Dropdown>(true);
            EnemyTypeSelecter = childs[0];
            EnemyTypeSelecter.ClearOptions();
            EnemyTypeSelecter.AddOptions(options.ToList());
            EnemyTypeSelecter.value = (int)type;
        }
    }
}