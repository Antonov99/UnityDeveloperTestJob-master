using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class EnemyInstaller : MonoInstaller
    {
        [SerializeField]
        private Transform _spawnPointTransform;
        
        [SerializeField]
        private float _cooldown;
        
        [SerializeField]
        private Entity _enemyPrefab;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<EnemyManager>()
                .AsSingle()
                .WithArguments(_spawnPointTransform)
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<EnemyCooldownSpawner>()
                .AsSingle()
                .WithArguments(_cooldown)
                .NonLazy();

            Container.BindMemoryPool<Entity, MonoMemoryPool<Entity>>()
                .WithInitialSize(10)
                .FromComponentInNewPrefab(_enemyPrefab)
                .UnderTransformGroup("Enemies")
                .NonLazy();
        }
    }
}