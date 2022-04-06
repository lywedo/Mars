using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class Global : MonoBehaviour
    {
        private static Global Instance;

        public static Global GetInstance()
        {
            return Instance;
        }
        private void Awake()
        {
            if (Instance != null) return;
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            Debug.Log("global start");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Back2Mars();
            }
        }

        public async void Back2Mars()
        {
            if (SceneManager.GetActiveScene().name != "mars ball")
            {
                await SceneChangeHelper.PreChangeSceneAsync("mars ball");
                Destroy(gameObject);
                await SceneChangeHelper.ChangeSceneAsync();
            }
            
        }
    }
}