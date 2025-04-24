using System;
using JetBrains.Annotations;

namespace Gameplay
{
    [UsedImplicitly]
    public class HealthComponent
    {
        public event Action<Entity> OnDeath;

        private int _health;
        
        private readonly int _maxHealth;
        private readonly Entity _entity;

        public HealthComponent(int maxHealth, Entity entity)
        {
            _maxHealth = maxHealth;
            _entity = entity;
        }

        public void SetMaxHealth()
        {
            _health = _maxHealth;
        }

        public void Damage(int damage)
        {
            _health -= damage;
            
            if (_health<=0)
                OnDeath?.Invoke(_entity);
        }
    }
}