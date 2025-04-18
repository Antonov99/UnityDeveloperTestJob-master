using System;
using UnityEngine;

namespace Gameplay
{
    public class Projectile : MonoBehaviour
    {
        public event Action<Projectile> OnCollisionEntered;
        
        [SerializeField]
        private float _speed = 0.2f;

        [SerializeField]
        private int _damage = 10;

        private Transform _target;

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        private void Update()
        {
            var translation = transform.forward * _speed;
            transform.Translate(translation);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Entity entity))
                entity.Get<HealthComponent>()?.Damage(_damage);
            
            OnCollisionEntered?.Invoke(this);
        }

        public void SetTarget(Transform target)
        {
            _target = target;
        }
    }
}