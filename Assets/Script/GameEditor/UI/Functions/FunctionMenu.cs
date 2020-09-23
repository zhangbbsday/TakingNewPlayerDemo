using System.Collections;
using UnityEngine;

namespace GameEditor
{
    public class FunctionMenu : UIElementBase
    {
        protected override void Awake()
        {
            base.Awake();
            new NewCoroutine(AutoClose()).StartCoroutine();
        }

        public void Show()
        {
            GameObject.SetActiveNew(true);
        }

        public void Close()
        {
            GameObject.SetActiveNew(false);
        }

        private IEnumerator AutoClose()
        {
            yield return null;
            Close();
        }
    }
}
