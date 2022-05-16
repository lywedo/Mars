using ET;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class SceneChangeHelper
    {
        public static AsyncOperation loadMapOperation;

        public static async ETTask PreChangeSceneAsync(string sceneName)
        {
            // 加载map
            loadMapOperation = SceneManager.LoadSceneAsync(sceneName);
            loadMapOperation.allowSceneActivation = false;
            Debug.Log("fifnishet===");
            await ETTask.CompletedTask;
            Debug.Log("fifnishet");
        }
        public static async ETTask ChangeSceneAsync()
        {
            loadMapOperation.allowSceneActivation = true;
            await ETTask.CompletedTask;
        }
    }
}