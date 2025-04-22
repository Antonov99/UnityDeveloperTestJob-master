using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class ProjectileEntityInstaller : MonoInstaller
    {
        [SerializeField]
        private Rigidbody _rigidbody;

        [SerializeField]
        private MoveSpeedConfig _moveSpeedConfig;
        
        public override void InstallBindings()
        {
            Container.Bind<MoveComponent>().AsSingle().WithArguments(_rigidbody, _moveSpeedConfig).NonLazy();
        }
    }
}