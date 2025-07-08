using System;
using Characters.Interfaces;
using Infrastructure;

namespace Characters.Proxies {
    public class EnemyProxy : Proxy<Enemy>, IEnemy {
        public event Action OnDeath {
            add =>
                Subject.OnDeath += value;
            remove =>
                Subject.OnDeath -= value;
        }
    }
}