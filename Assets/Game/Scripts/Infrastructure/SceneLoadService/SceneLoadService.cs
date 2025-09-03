using System;
using System.Collections;
using Infrastructure.CoroutineService;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure.SceneLoadService
{
    public sealed class SceneLoadService : ISceneLoadService
    {
        private readonly ICoroutineService _coroutineService;

        public SceneLoadService(ICoroutineService coroutineService)
        {
            _coroutineService = coroutineService;
        }

        void ISceneLoadService.Load(string name, Action onLoaded)
        {
            _coroutineService.StartCoroutine(LoadScene(name, onLoaded));
        }

        private static IEnumerator LoadScene(string name, Action onLoaded)
        {
            if (SceneManager.GetActiveScene().name.Equals(name))
            {
                onLoaded?.Invoke();
                
                yield break;
            }

            AsyncOperation handle = SceneManager.LoadSceneAsync(name, LoadSceneMode.Single);
            
            while (handle?.isDone == false)
            {
                yield return null;
            }
            
            onLoaded?.Invoke();
        }
    }
}