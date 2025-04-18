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

        public TowerAttackSystem(float delay, EnemyManager enemyManager, Transform myTransform, float range)
        {
            _enemyManager = enemyManager;
            _myTransform = myTransform;
            _range = range;
            _timer = new Timer(delay);
        }

        void ITickable.Tick()
        {
            if (!FindNearestEnemy(out Transform target))
                return;

            Fire(target);
        }

        private void Fire(Transform target)
        {
            if (_timer.IsPlaying) return;
            
            _fireComponent.Shoot(target);
            _timer.Play();
            _timer.OnFinished += OnFinished;
        }

        private void OnFinished()
        {
            _timer.OnFinished -= OnFinished;
        }

        //Проверить
        private bool FindNearestEnemy(out Transform transform)
        {
            transform = null;
            
            var targets = _enemyManager.GetActiveEnemies();
            var minDistance = _range;
            
            foreach (var target in targets)
            {
                var distance = Vector3.Distance(_myTransform.position,target.transform.position);
                
                if (distance>minDistance) 
                    continue;

                minDistance = distance;
                transform = target.transform;
            }

            return transform != null;
        }
    }
}