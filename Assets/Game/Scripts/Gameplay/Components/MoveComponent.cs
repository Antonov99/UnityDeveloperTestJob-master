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

        public void Move(Vector3 targetPosition)
        {
            Vector3 direction = targetPosition - _rigidbody.position;

            if (direction.magnitude < 0.1f)
            {
                _rigidbody.velocity = Vector3.zero;
                return;
            }

            direction.Normalize();

            float inputMagnitude = direction.magnitude;
            float speedMultiplier = Mathf.Pow(inputMagnitude, _accelerationFactor);
            float dynamicSpeed = Mathf.Lerp(_minSpeed, _maxSpeed, speedMultiplier);

            float finalSpeed = dynamicSpeed * _baseSpeed / _maxSpeed;

            _rigidbody.velocity = direction * finalSpeed;
        }
    }
}