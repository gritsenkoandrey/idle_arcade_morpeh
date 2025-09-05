using System.Runtime.CompilerServices;
using Unity.Cinemachine;
using UnityEngine;

namespace Infrastructure.CameraService
{

    public sealed class CameraService : MonoBehaviour, ICameraService
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private CinemachineCamera _virtualCamera;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Camera ICameraService.GetCamera() => _camera;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void ICameraService.SetTarget(Transform target) => SetTarget(target);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        float ICameraService.GetEulerAngleY() => _camera.transform.eulerAngles.y;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SetTarget(Transform target)
        {
            _virtualCamera.Target.TrackingTarget = target;
            _virtualCamera.Target.LookAtTarget = target;
        }
    }
}