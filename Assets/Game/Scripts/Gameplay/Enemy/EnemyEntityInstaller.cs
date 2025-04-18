using UnityEngine;
using UnityEngine.Serialization;
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

        public override void InstallBindings()
        {
            Container.Bind<HealthComponent>().AsSingle().WithArguments(_health).NonLazy();
            Container.Bind<MoveComponent>().AsSingle().WithArguments(_rigidbody, _moveSpeedConfig).NonLazy();
        }
    }
}