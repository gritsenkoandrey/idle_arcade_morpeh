using UnityEngine;

namespace Infrastructure.CameraService
{
    public interface ICameraService
    {
        Camera GetCamera();
        void SetTarget(Transform target);
        float GetEulerAngleY();
    }
}