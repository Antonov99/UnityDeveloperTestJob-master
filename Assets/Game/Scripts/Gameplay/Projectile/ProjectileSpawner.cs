using JetBrains.Annotations;
using Zenject;

namespace Gameplay
{
    [UsedImplicitly]
    public sealed class ProjectileSpawner
    {
        private readonly MonoMemoryPool<Projectile> _projectilePool;
        
        public ProjectileSpawner(MonoMemoryPool<Projectile> projectilePool)
        {
            _projectilePool = projectilePool;
        }

        public Projectile SpawnProjectile()
        {
            var projectile = _projectilePool.Spawn();

            return projectile;
        }

        public void RemoveProjectile(Projectile projectile)
        {
            _projectilePool.Despawn(projectile);
        }
    }
}