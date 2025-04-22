using System;
using JetBrains.Annotations;
using Zenject;

namespace Gameplay
{
    [UsedImplicitly]
    public class EnemyDeathController : IInitializable, IDisposable
    {
        private readonly EnemyManager _enemyManager;
        private readonly HealthComponent _healthComponent;

        public EnemyDeathController(EnemyManager enemyManager, HealthComponent healthComponent)
        {
            _enemyManager = enemyManager;
            _healthComponent = healthComponent;
        }

        void IInitializable.Initialize()
        {
            _healthComponent.OnDeath += OnDeath;
        }

        private void OnDeath(Entity enemy)
        {
            _enemyManager.Despawn(enemy);
        }


        void IDisposable.Dispose()
        {
            _healthComponent.OnDeath -= OnDeath;
        }
    }
}