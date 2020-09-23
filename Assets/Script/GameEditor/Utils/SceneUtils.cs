using UnityEngine.SceneManagement;

namespace GameEditor
{
    public static class SceneUtils
    {
        public static void ChangeScene(string name)
        {
            CoroutineUtils.StopAllCoroutine();
            SceneManager.LoadScene(name);
        }
    }
}
