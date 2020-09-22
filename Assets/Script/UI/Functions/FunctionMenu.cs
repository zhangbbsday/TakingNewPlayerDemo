using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameEditor
{
    public class FunctionMenu : UIElementBase
    {
        protected override void Start()
        {
            base.Start();
            Close();
        }

        public void Show()
        {
            GameObject.SetActive(true);
        }

        public void Close()
        {
            GameObject.SetActive(false);
        }
    }
}
