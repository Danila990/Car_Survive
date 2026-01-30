using UnityEngine;


namespace _Project
{
    public class CarEngine : MonoBehaviour
    {
        // Settings
        public float MoveSpeed = 50;
        public float MaxSpeed = 15;
        public float Drag = 0.98f;
        public float SteerAngle = 20;
        public float Traction = 1;

        // Variables
        private Rigidbody _rigidbody;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            _rigidbody.linearVelocity += transform.forward * MoveSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
            transform.position += _rigidbody.linearVelocity * Time.deltaTime; 

            // Steering
            float steerInput = Input.GetAxis("Horizontal");
            transform.Rotate(Vector3.up * steerInput * _rigidbody.linearVelocity.magnitude * SteerAngle * Time.deltaTime);

            // Drag and max speed limit
            _rigidbody.linearVelocity *= Drag;
            _rigidbody.linearVelocity = Vector3.ClampMagnitude(_rigidbody.linearVelocity, MaxSpeed);

            _rigidbody.linearVelocity = Vector3.Lerp(_rigidbody.linearVelocity.normalized, transform.forward, Traction * Time.deltaTime) * _rigidbody.linearVelocity.magnitude;
        }
    }
}