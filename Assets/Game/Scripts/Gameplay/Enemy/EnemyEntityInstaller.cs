using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class EnemyEntityInstaller : MonoInstaller
    {
        [SerializeField]
        private int _health;

        [SerializeField]
        private Rigidbody _rigidbody;

        [SerializeField]
        private MoveSpeedConfig _moveSpeedConfig;

        [SerializeField]
        private Vector3 _targetPoint;
        
        [SerializeField]
        private Entity _entity;

        public override void InstallBindings()
        {
            //Components
            Container.Bind<HealthComponent>().AsSingle().WithArguments(_health).NonLazy();
            Container.Bind<MoveComponent>().AsSingle().WithArguments(_rigidbody, _moveSpeedConfig).NonLazy();
            
            //Systems
            Container.BindInterfacesAndSelfTo<EnemyDeathController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<EnemyPatrolMechanic>().AsSingle().WithArguments(_targetPoint).NonLazy();
            Container.BindInterfacesAndSelfTo<EnemyFinishObserver>().AsSingle().NonLazy();
            
            //ETC
            Container.Bind<Entity>().FromInstance(_entity).AsSingle().NonLazy();
        }
    }
}