using System;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    [UsedImplicitly]
    public class EnemyFinishObserver : IInitializable, IDisposable
    {
        private readonly EnemyPatrolMechanic _enemyPatrolMechanic;
        private readonly EnemyManager _enemyManager;

        public EnemyFinishObserver(EnemyPatrolMechanic enemyPatrolMechanic, EnemyManager enemyManager)
        {
            _enemyPatrolMechanic = enemyPatrolMechanic;
            _enemyManager = enemyManager;
        }

        void IInitializable.Initialize()
        {
            _enemyPatrolMechanic.OnFinishMovement += OnFinishMovement;
        }

        private void OnFinishMovement(Entity enemy)
        {
            Debug.Log($"Враг: {enemy} добрался до финиша");
            _enemyManager.Despawn(enemy);
        }

        void IDisposable.Dispose()
        {
            _enemyPatrolMechanic.OnFinishMovement -= OnFinishMovement;
        }
    }
}