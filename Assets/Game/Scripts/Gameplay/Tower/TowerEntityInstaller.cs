using UnityEngine;
using Zenject;

namespace Gameplay
{
    public sealed class TowerEntityInstaller : MonoInstaller
    {
        [SerializeField]
        public Transform _shootPoint;

        [SerializeField]
        private Rigidbody _rigidbody;

        [SerializeField]
        private float _rotationSpeed;

        [SerializeField]
        private float _shootInterval;

        [SerializeField]
        public float _range = 4f;

        [SerializeField]
        private Projectile _projectilePrefab;

        [SerializeField]
        private string _nameOfTransformGroup;

        [SerializeField]
        private Transform _myTransform;

        public override void InstallBindings()
        {
            //Components:
            Container
                .Bind<FireComponent>()
                .AsSingle()
                .WithArguments(_shootPoint)
                .NonLazy();

            Container
                .Bind<RotationComponent>()
                .AsSingle()
                .WithArguments(_rigidbody, _rotationSpeed)
                .NonLazy();

            //Systems:
            Container
                .BindInterfacesAndSelfTo<TowerAttackSystem>()
                .AsSingle()
                .WithArguments(_shootInterval, _myTransform, _range)
                .NonLazy();

            Container
                .Bind<ProjectileSpawner>()
                .AsSingle()
                .NonLazy();

            Container
                .Bind<ProjectileManager>()
                .AsSingle()
                .NonLazy();

            //ETC:
            Container.BindMemoryPool<Projectile, MonoMemoryPool<Projectile>>()
                .WithInitialSize(5)
                .FromComponentInNewPrefab(_projectilePrefab)
                .UnderTransformGroup(_nameOfTransformGroup)
                .NonLazy();
        }

        /*void Update()
        {
            if (m_projectilePrefab == null || m_shootPoint == null)
                return;

            foreach (var monster in FindObjectsOfType<Monster>())
            {
                if (Vector3.Distance(transform.position, monster.transform.position) > m_range)
                    continue;

                if (m_lastShotTime + m_shootInterval > Time.time)
                    continue;

                // shot
                Instantiate(m_projectilePrefab, m_shootPoint.position, m_shootPoint.rotation);

                m_lastShotTime = Time.time;
            }
        }*/
    }
}