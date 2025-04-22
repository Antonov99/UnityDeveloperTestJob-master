using JetBrains.Annotations;
using UnityEngine;

namespace Gameplay
{
    [UsedImplicitly]
    public class RotationComponent
    {
        private readonly Rigidbody _rigidbody;
        private readonly float _rotationSpeed;

        public RotationComponent(Rigidbody rigidbody, float rotationSpeed)
        {
            _rigidbody = rigidbody;
            _rotationSpeed = rotationSpeed;
        }

        public void Rotate(Vector3 direction)
        {
            if (direction == Vector3.zero)
                return;

            if (!(direction.sqrMagnitude > 0))
                return;

            var lookDirection = new Vector3(direction.x, 0, direction.z);
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
            _rigidbody.MoveRotation(Quaternion.Slerp(_rigidbody.rotation, targetRotation,
                _rotationSpeed * Time.fixedDeltaTime));
        }
    }
}