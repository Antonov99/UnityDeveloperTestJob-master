using System;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    [UsedImplicitly]
    public class EnemyPatrolMechanic : IFixedTickable
    {
        public event Action<Entity> OnFinishMovement;

        private readonly MoveComponent _moveComponent;
        private readonly Vector3 _targetPoint;
        private readonly Entity _entity;

        public EnemyPatrolMechanic(MoveComponent moveComponent, Vector3 targetPoint, Entity entity)
        {
            _moveComponent = moveComponent;
            _targetPoint = targetPoint;
            _entity = entity;
        }

        public void FixedTick()
        {
            var position = _moveComponent.Position;
            if (Vector3.Distance(position, _targetPoint) < 1f)
                OnFinishMovement?.Invoke(_entity);
            
            _moveComponent.Move(_targetPoint);
        }
    }
}