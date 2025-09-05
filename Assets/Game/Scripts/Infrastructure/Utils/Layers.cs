using UnityEngine;

namespace Infrastructure.Utils
{
    public static class Layers
    {
        public static readonly int Default = LayerMask.NameToLayer("Default");
        public static readonly int GroundShift = 1 << LayerMask.NameToLayer("Ground");
    }
}