using JetBrains.Annotations;
using UnityEngine;

namespace Gameplay
{
    [UsedImplicitly]
    public class FireComponent
    {
        private readonly Transform _shootPoint;
        private readonly ProjectileManager _projectileManager;
        
        public FireComponent(Transform shootPoint, ProjectileManager projectileManager)
        {
            _shootPoint = shootPoint;
            _projectileManager = projectileManager;
        }

        public void Shoot(Transform transform)
        {
            var projectile = _projectileManager.Spawn();
            projectile.SetPosition(_shootPoint.position);
            projectile.SetTarget(transform);
        }
    }
}