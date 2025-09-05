using UnityEngine;

namespace Infrastructure.JoystickService
{
    public interface IJoystickService
    {
        Vector2 GetAxis();
        bool HasInput();
        void Init();
        void Enable(bool isEnable);
        void Execute();
    }
}