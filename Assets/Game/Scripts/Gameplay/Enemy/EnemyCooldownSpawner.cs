using Elementary;
using JetBrains.Annotations;
using Zenject;

namespace Gameplay
{
    [UsedImplicitly]
    public class EnemyCooldownSpawner : ITickable
    {
        private readonly EnemyManager _enemyManager;
        private readonly Timer _timer;

        public EnemyCooldownSpawner(EnemyManager enemyManager, float cooldown)
        {
            _enemyManager = enemyManager;

            _timer = new Timer(cooldown);
        }

        void ITickable.Tick()
        {
            if (_timer.IsPlaying) return;

            _enemyManager.Spawn();
            _timer.ResetTime();
            _timer.Play();
        }
    }
}