using System;
using UnityEngine;
using UnityEngine.Video;

namespace DefaultNamespace
{
    public class InitController : MonoBehaviour
    {
        public VideoPlayer VideoPlayer;

        private void Start()
        {
            VideoPlayer.Prepare();
            VideoPlayer.Play();
            SceneChangeHelper.PreChangeSceneAsync("mars ball").Coroutine();
            VideoPlayer.loopPointReached += source =>
            {
                Debug.Log("bowanle");
                SceneChangeHelper.ChangeSceneAsync().Coroutine();
            };
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                VideoPlayer.Stop();
                SceneChangeHelper.ChangeSceneAsync().Coroutine();
            }
        }
    }
}