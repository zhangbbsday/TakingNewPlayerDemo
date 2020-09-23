using UnityEngine;

namespace GameEditor
{
    public class FunctionMenu : UIElementBase
    {
        protected override void Awake()
        {
            base.Awake();
            Close();
        }

        public void Show()
        {
            GameObject.SetActiveNew(true);
        }

        public void Close()
        {
            GameObject.SetActiveNew(false);
        }
    }
}
