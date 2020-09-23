using UnityEngine;

namespace GameEditor
{
    public static class GameObjectUtils
    {
        public static void SetActiveNew(this GameObject go, bool state)
        {
            if (go == null)
                return;

            if (go.activeSelf != state)
            {
                go.SetActive(state);
            }
        }
    }
}
