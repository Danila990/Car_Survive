using UnityEngine;

namespace _Project
{
    public class CarEngine : MonoBehaviour
    {
        [Header("Настройки Передвижение")]
        [SerializeField] private float _moveSpeed = 50;
        [SerializeField] private float _maxSpeed = 15;
        [SerializeField] private float _steerAngle = 20;
        [SerializeField] private float _traction = 1;

        [Header("Настройки эффектов")]

        [Header("Настройки Звука")]

        private Rigidbody _rigidbody;

        private float _xInput;
        private float _yInput;
        private bool _spaceInput;

        private bool _isDrifting;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            InputHandler();
        }

        private void FixedUpdate()
        {
            Movement();
            Rotation();
            ForwardInertia();
            MoveLimit();
            Drifting();
        }

        private void Drifting()
        {
            
        }

        private void Movement()
        {
            _rigidbody.linearVelocity += transform.forward * _moveSpeed * _yInput * Time.fixedDeltaTime;
        }

        private void Rotation()
        {
            Quaternion rotate = Quaternion.Euler(0, _xInput * _rigidbody.linearVelocity.magnitude * _steerAngle * Time.fixedDeltaTime, 0);
            _rigidbody.rotation = Quaternion.Slerp(_rigidbody.rotation, _rigidbody.rotation * rotate, 10 * Time.fixedDeltaTime);
        }

        private void ForwardInertia()
        {
            _rigidbody.linearVelocity = Vector3.Lerp(_rigidbody.linearVelocity.normalized, transform.forward, _traction * Time.fixedDeltaTime) * _rigidbody.linearVelocity.magnitude;
        }

        private void MoveLimit()
        {
            _rigidbody.linearVelocity *= 0.98f;
            _rigidbody.linearVelocity = Vector3.ClampMagnitude(_rigidbody.linearVelocity, _maxSpeed);
        }

        private void InputHandler()
        {
            _xInput = Input.GetAxis("Horizontal");
            _yInput = Input.GetAxis("Vertical");
            _spaceInput = Input.GetKey(KeyCode.Space);
        }
    }
}