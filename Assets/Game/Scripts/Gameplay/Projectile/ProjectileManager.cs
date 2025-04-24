using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Gameplay
{
    [UsedImplicitly]
    public class ProjectileManager
    {
        private readonly HashSet<Projectile> _activeBullets = new();

        private readonly ProjectileSpawner _projectileSpawner;

        public ProjectileManager(ProjectileSpawner projectileSpawner)
        {
            _projectileSpawner = projectileSpawner;
        }

        public Projectile Spawn()
        {
            var projectile = _projectileSpawner.SpawnProjectile();
            if (_activeBullets.Add(projectile))
            {
                projectile.OnCollisionEntered += Despawn;
            }

            return projectile;
        }

        public void Despawn(Projectile projectile)
        {
            Debug.Log("Despawn");

            if (!_activeBullets.Remove(projectile)) return;
            
            projectile.OnCollisionEntered -= Despawn;
            _projectileSpawner.RemoveProjectile(projectile);
        }
    }
}