using System.Collections.Generic;

namespace Gameplay
{
    public class EnemyManager
    {
        private List<Monster> _activeEnemies;

        public IEnumerable<Monster> GetActiveEnemies()
        {
            return _activeEnemies;
        }
    }
}