using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

namespace WinterUniverse
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : NetworkBehaviour
    {
        [SerializeField] private Transform _head;
        [SerializeField] private float _acceleration = 8f;
        [SerializeField] private float _deceleration = 16f;
        [SerializeField] private float _moveSpeed = 4f;
        [SerializeField] private float _lookSpeed = 10f;
        [SerializeField] private float _maxLookAngle = 75f;

        private CharacterController _cc;
        private Vector3 _moveVelocity;
        private Vector3 _fallVelocity;
        private Vector2 _moveInput;
        private Vector2 _lookInput;
        private float _lookAngle;

        public void OnMove(InputValue value)
        {
            if (!IsOwner)
            {
                return;
            }
            _moveInput = value.Get<Vector2>();
        }

        public void OnLook(InputValue value)
        {
            if (!IsOwner)
            {
                return;
            }
            _lookInput = value.Get<Vector2>();
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            _cc = GetComponent<CharacterController>();
            if (IsOwner)
            {
                FindFirstObjectByType<CameraController>().SetTarget(_head);
            }
        }

        private void Update()
        {
            if (!IsOwner)
            {
                return;
            }
            if (_moveInput != Vector2.zero)
            {
                _moveVelocity = Vector3.MoveTowards(_moveVelocity, (transform.right * _moveInput.x + transform.forward * _moveInput.y).normalized * _moveSpeed, _acceleration * Time.deltaTime);
            }
            else
            {
                _moveVelocity = Vector3.MoveTowards(_moveVelocity, Vector3.zero, _deceleration * Time.deltaTime);
            }
            if (_lookInput != Vector2.zero)
            {
                transform.Rotate(Vector3.up * _lookInput.x * _lookSpeed * Time.deltaTime);
                _lookAngle = Mathf.Clamp(_lookAngle - (_lookInput.y * _lookSpeed * Time.deltaTime), -_maxLookAngle, _maxLookAngle);
                _head.localRotation = Quaternion.Euler(_lookAngle, 0f, 0f);
            }
            _cc.Move(_moveVelocity * Time.deltaTime);
            _cc.Move(_fallVelocity * Time.deltaTime);
        }
    }
}