using Scellecs.Morpeh;
using VContainer;

namespace Infrastructure.Scopes.EntryPoints
{
    public sealed class EntryPointMenuSystem : BaseEntryPointSystem
    {
        public EntryPointMenuSystem(IObjectResolver objectResolver) : base(objectResolver)
        {
        }

        protected override ISystem[] CreateGameSystems()
        {
            return null;
        }
    }
}