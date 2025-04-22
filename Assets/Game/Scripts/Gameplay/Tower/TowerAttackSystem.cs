using Elementary;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    [UsedImplicitly]
    public sealed class TowerAttackSystem : ITickable
    {
        private readonly Timer _timer;

        private readonly FireComponent _fireComponent;
        private readonly EnemyManager _enemyManager;
        private readonly Transform _myTransform;
        private readonly float _range;
        private readonly RotationComponent _rotationComponent;

        public TowerAttackSystem(
            float delay,
            FireComponent fireComponent,
            EnemyManager enemyManager,
            Transform myTransform,
            float range, RotationComponent rotationComponent)
        {
            _timer = new Timer(delay);

            _fireComponent = fireComponent;
            _enemyManager = enemyManager;
            _myTransform = myTransform;
            _range = range;
            _rotationComponent = rotationComponent;
        }

        void ITickable.Tick()
        {
            if (!FindNearestEnemy(out Transform target))
                return;

            _rotationComponent.Rotate(target.position);
            Fire(target);
        }

        private void Fire(Transform target)
        {
            if (_timer.IsPlaying) return;

            _fireComponent.Shoot(target);
            _timer.ResetTime();
            _timer.Play();
        }

        //Проверить
        private bool FindNearestEnemy(out Transform transform)
        {
            transform = null;

            var targets = _enemyManager.GetActiveEnemies();
            var minDistance = _range;

            foreach (var target in targets)
            {
                var distance = Vector3.Distance(_myTransform.position, target.transform.position);

                if (distance > minDistance)
                    continue;

                minDistance = distance;
                transform = target.transform;
            }

            return transform != null;
        }
    }
}