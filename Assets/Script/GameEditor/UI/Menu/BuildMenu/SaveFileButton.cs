using System;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace GameEditor
{
    public class SaveFileButton : ButtonBase
    {
        protected override Action AddMethod()
        {
            return () => PressAction();
        }

        protected override ButtonManager.ButtonEffectType SetButtonEffect()
        {
            return ButtonManager.ButtonEffectType.NormalEffect;
        }

        public void PressAction()
        {
            var input = Transform.parent.Find("Input").GetComponent<InputField>();
            string path = GetInnocentPath(input.text);

            GameManager.Instance.BuildManager.SaveFile(path);
        }

        private string GetInnocentPath(string path)
        {
            StringBuilder builder = new StringBuilder(path);
            foreach (char c in Path.GetInvalidFileNameChars())
                builder = builder.Replace(c.ToString(), string.Empty);

            return builder.ToString();
        }
    }
}