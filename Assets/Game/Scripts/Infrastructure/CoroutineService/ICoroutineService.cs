using System.Collections;
using UnityEngine;

namespace Infrastructure.CoroutineService
{
    public interface ICoroutineService
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
        void StopCoroutine(Coroutine coroutine);
        void StopAllCoroutines();
    }
}