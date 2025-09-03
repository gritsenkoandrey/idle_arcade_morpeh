using System;

namespace Infrastructure.SceneLoadService
{
    public interface ISceneLoadService
    {
        void Load(string name, Action onLoaded);
    }
}