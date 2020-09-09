using System;
using System.Collections.Generic;
using System.Text;

namespace SamuraiApp.Domain
{
    public class Battle
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        // This list creates a connection to the joining table between Battle and Samurai
        public List<SamuraiBattle> SamuraiBattles { get; set; }
    }
}
