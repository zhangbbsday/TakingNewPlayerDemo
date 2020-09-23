using System.Collections;
using UnityEngine;

namespace GameEditor
{
    public class NormalButtonEffect : IButtonEffect
    {
        private float EffectTime { get; } = 0.25f;
        private float SizeChangeSpeed { get; } = 0.5f;
        private Color32 SelectedColor { get; } = new Color32(0, 255, 203, 130);
        private Color32 DefaultColor { get; } = new Color32(0, 255, 203, 0);
        private ButtonBase Button { get; }

        public NormalButtonEffect(ButtonBase button)
        {
            Button = button;
        }

        public void EnterEffect()
        {
            NewCoroutine coroutine = new NewCoroutine(EnterAction());
            coroutine.StartCoroutine();
        }

        public void ExitEffect()
        {
            NewCoroutine coroutine = new NewCoroutine(ExitAction());
            coroutine.StartCoroutine();
        }

        public void PressEffect()
        {
            NewCoroutine coroutine = new NewCoroutine(PressAction());
            coroutine.StartCoroutine();
        }

        public void ReleaseEffect()
        {
            //暂时没有效果
        }

        public void SelectedEffect()
        {
            NewCoroutine coroutine = new NewCoroutine(SelectedAction());
            coroutine.StartCoroutine();
        }

        public void CancelEffect()
        {
            NewCoroutine coroutine = new NewCoroutine(CancelAction());
            coroutine.StartCoroutine();
        }


        private IEnumerator EnterAction()
        {
            float startTime = Time.time;
            while (Time.time - startTime <= EffectTime)
            {
                if (!IsButtonNull())
                    Button.TextTransform.localScale += new Vector3(1, 1, 0) * SizeChangeSpeed * Time.deltaTime;
                yield return null;
            }
        }

        private IEnumerator ExitAction()
        {
            float startTime = Time.time;
            while (Time.time - startTime <= EffectTime)
            {
                if (!IsButtonNull())
                    Button.TextTransform.localScale -= new Vector3(1, 1, 0) * SizeChangeSpeed * Time.deltaTime;
                yield return null;
            }
            if (!IsButtonNull())
                Button.TextTransform.localScale = Vector3.one;
        }

        private IEnumerator PressAction()
        {
            float startTime = Time.time;
            while (Time.time - startTime <= EffectTime / 2)
            {
                if (!IsButtonNull())
                    Button.TextTransform.localScale -= new Vector3(1, 1, 0) * SizeChangeSpeed * Time.deltaTime;
                yield return null;
            }

            startTime = Time.time;
            while (Time.time - startTime <= EffectTime / 2)
            {
                if (!IsButtonNull())
                    Button.TextTransform.localScale += new Vector3(1, 1, 0) * SizeChangeSpeed * Time.deltaTime;
                yield return null;
            }
        }

        private IEnumerator SelectedAction()
        {
            float startTime = Time.time;
            while (Time.time - startTime <= EffectTime)
            {
                if (!IsButtonNull())
                    Button.Button.image.color = Color32.Lerp(Button.Button.image.color, SelectedColor, 0.5f);
                yield return null;
            }
            if (!IsButtonNull())
                Button.Button.image.color = SelectedColor;
        }

        private IEnumerator CancelAction()
        {
            if (!IsButtonNull())
                Button.Button.image.color = DefaultColor;
            yield return null;
        }

        private bool IsButtonNull()
        {
            return Button == null;
        }
    }
}