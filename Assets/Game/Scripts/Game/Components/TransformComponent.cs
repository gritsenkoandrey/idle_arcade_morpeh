using System;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Game.Components
{
    [System.Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct TransformComponent : IComponent, IDisposable
    {
        public Transform value;
        public void SetParent(Transform parent, bool worldPositionStays = true) => value.SetParent(parent, worldPositionStays);
        public void Dispose() => UnityEngine.Object.Destroy(value.gameObject);
    }
}