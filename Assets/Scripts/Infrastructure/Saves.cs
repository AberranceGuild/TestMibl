using System;
using Infrastructure.Data;

namespace Infrastructure {
    [Serializable]
    public class Saves {
        public GameData GameData = new();
        public BulletData BulletData = new();
    }
}