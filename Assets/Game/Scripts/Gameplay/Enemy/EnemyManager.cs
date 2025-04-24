using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    [UsedImplicitly]
    public class EnemyManager
    {
        private readonly List<Entity> _activeEnemies=new();

        private readonly MonoMemoryPool<Entity> _enemyPool;
        private readonly Transform _spawnPointTransform;

        public EnemyManager(MonoMemoryPool<Entity> enemyPool, Transform spawnPointTransform)
        {
            _enemyPool = enemyPool;
            _spawnPointTransform = spawnPointTransform;
        }

        public void Spawn()
        {
            var enemy = _enemyPool.Spawn();
            if (enemy is null) throw new NullReferenceException();
            _activeEnemies.Add(enemy);
            enemy.transform.position = _spawnPointTransform.position;
            enemy.Get<HealthComponent>().SetMaxHealth();
        }

        public void Despawn(Entity entity)
        {
            if (_activeEnemies.Remove(entity))
                _enemyPool.Despawn(entity);
        }

        public IEnumerable<Entity> GetActiveEnemies()
        {
            return _activeEnemies;
        }
    }
}