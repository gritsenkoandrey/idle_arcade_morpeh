using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Game.Events
{
    [System.Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct TriggerEvent : IEventData
    {
        public Entity source;
        public Entity target;

        public TriggerEvent(Entity source, Entity target)
        {
            this.source = source;
            this.target = target;
        }
    }
}