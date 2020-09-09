using System;
using System.Collections.Generic;
using System.Text;

namespace SamuraiApp.Domain
{
    // Joining table for Samurai and Battle tables
    public class SamuraiBattle
    {
        public int SamuraiId { get; set; }
        public int BattleId { get; set; }
        public Samurai Samurai { get; set; }
        public Battle Battle { get; set; }
    }
}
