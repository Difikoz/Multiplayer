using UnityEngine;

namespace WinterUniverse
{
    public class CameraController : MonoBehaviour
    {
        private Transform _target;

        private void LateUpdate()
        {
            if (_target == null)
            {
                return;
            }
            transform.SetPositionAndRotation(_target.position, _target.rotation);
        }

        public void SetTarget(Transform target)
        {
            _target = target;
        }
    }
}