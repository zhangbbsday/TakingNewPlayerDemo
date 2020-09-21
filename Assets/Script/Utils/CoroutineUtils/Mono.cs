using UnityEngine;

public class Mono : MonoBehaviour
{
    public static Mono Instance { get; private set; }

    public static void CreateOne()
    {
        if (Instance != null)
            Destroy(Instance);

        Instance = new GameObject("Mono").AddComponent<Mono>();
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
