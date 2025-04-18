using JetBrains.Annotations;

namespace Gameplay
{
    [UsedImplicitly]
    public class HealthComponent
    {
        public bool IsAlive => _health > 0;
        
        private int _health;

        public HealthComponent(int health)
        {
            _health = health;
        }

        public void Damage(int damage)
        {
            _health -= damage;
        }
    }
}