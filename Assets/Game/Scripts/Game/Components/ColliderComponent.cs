using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Game.Components
{
    [System.Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct ColliderComponent : IComponent
    {
        public Collider value;
        public void Enable() => value.enabled = true;
        public void Disable() => value.enabled = false;
        public bool IsEnabled => value.enabled;
    }
}