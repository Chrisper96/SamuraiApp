using System;
using System.Collections.Generic;
using System.Text;

namespace SamuraiApp.Domain
{
    public class Samurai
    {
        public Samurai()
        {
            Quotes = new List<Quote>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Quote> Quotes { get; set; }
        public Clan Clan { get; set; }
        // This list creates a connection to the joining table between the Samurai and Battle tables
        public List<SamuraiBattle> SamuraiBattles { get; set; }
        // Navigation property for the Samurais Horse
        public Horse Horse { get; set; }
    }
}
