using System;
using UnityEngine;

namespace Gameplay
{
    public class Projectile : MonoBehaviour
    {
        public event Action<Projectile> OnCollisionEntered;
        
        [SerializeField]
        private int _damage = 10;

        [SerializeField]
        private Entity _entity;

        private MoveComponent _moveComponent;

        private Transform _target;

        private void Start()
        {
            _target = null;
            _moveComponent= _entity.Get<MoveComponent>();
        }

        private void Update()
        {
            if (_target is null) 
                OnCollisionEntered?.Invoke(this);
            else
                _moveComponent.Move(_target.position);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.TryGetComponent(out Entity entity)) return;
            var healthComponent = entity.TryGet<HealthComponent>();
            if (healthComponent is null) return;
            healthComponent.Damage(_damage);
            
            OnCollisionEntered?.Invoke(this);
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void SetTarget(Transform target)
        {
            _target = target;
        }
    }
}