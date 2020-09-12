using System.Collections;

public class NewCoroutine
{
    public bool IsRunning { get; set; }
    private IEnumerator Coroutine { get; }
    private IEnumerator Wrapper { get; }
    public NewCoroutine(IEnumerator coroutine)
    {
        Coroutine = coroutine;
        Wrapper = CallWrapper();
    }

    public IEnumerator GetNewCoroutine()
    {
        return Wrapper;
    }

    private IEnumerator CallWrapper()
    {
        if (Coroutine != null)
        {
            IsRunning = true;
            yield return null;

            while (IsRunning)
            {
                if (Coroutine.MoveNext())
                    yield return Coroutine.Current;
                else
                    break;
            }
        }
        
        IsRunning = false;
    }
}
