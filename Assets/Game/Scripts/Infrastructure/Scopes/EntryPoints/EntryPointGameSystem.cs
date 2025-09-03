using Scellecs.Morpeh;
using VContainer;

namespace Infrastructure.Scopes.EntryPoints
{
    public sealed class EntryPointGameSystem : BaseEntryPointSystem
    {
        public EntryPointGameSystem(IObjectResolver objectResolver) : base(objectResolver)
        {
        }

        protected override ISystem[] CreateGameSystems()
        {
            return null;
        }
    }
}