using System;
using Characters.Interfaces;
using Infrastructure;

namespace Characters.Proxies {
    public class PlayerProxy : Proxy<PlayerController2D>, IPlayer {
        public event Action OnDeath {
            add =>
                Subject.OnDeath += value;
            remove =>
                Subject.OnDeath -= value;
        }
    }
}