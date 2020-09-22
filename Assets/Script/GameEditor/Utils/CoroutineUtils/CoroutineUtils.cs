using UnityEngine;

namespace GameEditor
{
    public static class CoroutineUtils
    {
        private static MonoBehaviour Mono { get; set; }
        public static void StartCoroutine(this NewCoroutine coroutine)
        {
            if (Mono == null)
                return;

            coroutine.IsRunning = true;
            Mono.StartCoroutine(coroutine.GetNewCoroutine());
        }
        public static void StopCoroutine(this NewCoroutine coroutine)
        {
            if (Mono == null)
                return;

            coroutine.IsRunning = false;
            Mono.StopCoroutine(coroutine.GetNewCoroutine());
        }
        public static void StopAllCoroutine()
        {
            if (Mono == null)
                return;

            Mono.StopAllCoroutines();
        }

        public static bool IsCompleteOrEnd(this NewCoroutine coroutine)
        {
            return !coroutine.IsRunning;
        }

        public static void SetMono(Mono mono)
        {
            Mono = mono;
        }
    }
}