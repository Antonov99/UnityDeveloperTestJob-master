using System;
using JetBrains.Annotations;

namespace Gameplay
{
    [UsedImplicitly]
    public class HealthComponent
    {
        public event Action<Entity> OnDeath;
        
        private int _health;
        private readonly Entity _entity;

        public HealthComponent(int health, Entity entity)
        {
            _health = health;
            _entity = entity;
        }

        public void Damage(int damage)
        {
            _health -= damage;
            
            if (_health<=0)
                OnDeath?.Invoke(_entity);
        }
    }
}