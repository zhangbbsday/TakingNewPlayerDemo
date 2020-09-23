using UnityEngine;

namespace GameEditor
{
    public static class CoroutineUtils
    {
        private static bool HasCreartedMono { get; set; }
        private static MonoBehaviour MonoUtils { get; set; }
        public static void StartCoroutine(this NewCoroutine coroutine)
        {
            SetMono();

            if (MonoUtils == null)
                return;

            coroutine.IsRunning = true;
            MonoUtils.StartCoroutine(coroutine.GetNewCoroutine());
        }
        public static void StopCoroutine(this NewCoroutine coroutine)
        {
            SetMono();

            if (MonoUtils == null)
                return;

            coroutine.IsRunning = false;
            MonoUtils.StopCoroutine(coroutine.GetNewCoroutine());
        }
        public static void StopAllCoroutine()
        {
            SetMono();

            if (MonoUtils == null)
                return;

            MonoUtils.StopAllCoroutines();
        }

        public static bool IsCompleteOrEnd(this NewCoroutine coroutine)
        {
            return !coroutine.IsRunning;
        }

        public static void SetMono()
        {
            if (Mono.Instance != null || HasCreartedMono)
                return;

            Mono.CreateOne();
            MonoUtils = Mono.Instance;
            HasCreartedMono = true;
        }
    }
}