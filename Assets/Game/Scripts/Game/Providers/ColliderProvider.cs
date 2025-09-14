using Game.Components;
using Scellecs.Morpeh.Providers;
using Unity.IL2CPP.CompilerServices;

namespace Game.Providers
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class ColliderProvider : MonoProvider<ColliderComponent>
    {
        public void Enable()
        {
            ref ColliderComponent data = ref GetData();

            data.Enable();
        }
        
        public void Disable()
        {
            ref ColliderComponent data = ref GetData();

            data.Disable();
        }
    }
}