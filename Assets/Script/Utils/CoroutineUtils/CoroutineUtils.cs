using UnityEngine;

public static class CoroutineUtils
{
    private static MonoBehaviour Mono { get; set; }
    public static void StartCoroutine(this NewCoroutine coroutine)
    {
        SetMono();
        coroutine.IsRunning = true;
        Mono.StartCoroutine(coroutine.GetNewCoroutine());
    }
    public static void StopCoroutine(this NewCoroutine coroutine)
    {
        SetMono();
        coroutine.IsRunning = false;
        Mono.StopCoroutine(coroutine.GetNewCoroutine());
    }
    public static void StopAllCoroutine()
    {
        Mono.StopAllCoroutines();
    }
    public static bool IsCompleteOrEnd(this NewCoroutine coroutine)
    {
        return !coroutine.IsRunning;
    }
    private static void SetMono()
    {
        if (Mono != null)
            return;

        Mono = new GameObject("Mono").AddComponent<Mono>();
    }
}
