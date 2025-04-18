using JetBrains.Annotations;
using UnityEngine;

namespace Gameplay
{
    [UsedImplicitly]
    public class MoveComponent
    {
        public Vector3 Position => _rigidbody.position;

        private readonly Rigidbody _rigidbody;
        private readonly float _baseSpeed;
        private readonly float _minSpeed;
        private readonly float _maxSpeed;
        private readonly float _accelerationFactor;

        public MoveComponent(Rigidbody rigidbody, MoveSpeedConfig moveSpeedConfig)
        {
            _rigidbody = rigidbody;
            _baseSpeed = moveSpeedConfig.BaseSpeed;
            _minSpeed = moveSpeedConfig.MinSpeed;
            _maxSpeed = moveSpeedConfig.MaxSpeed;
            _accelerationFactor = moveSpeedConfig.AccelerationFactor;
        }

        public void Move(Vector3 direction)
        {
            if (direction == Vector3.zero)
            {
                _rigidbody.velocity = Vector3.zero;
                return;
            }

            float inputMagnitude = direction.magnitude;
            if (inputMagnitude > 1) direction.Normalize();

            float speedMultiplier = Mathf.Pow(inputMagnitude, _accelerationFactor);
            float dynamicSpeed = Mathf.Lerp(_minSpeed, _maxSpeed, speedMultiplier);

            float finalSpeed = dynamicSpeed * _baseSpeed / _maxSpeed;

            _rigidbody.velocity = direction.normalized * finalSpeed;
        }
    }
}