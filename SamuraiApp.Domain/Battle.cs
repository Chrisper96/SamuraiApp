using System;
using System.Collections.Generic;
using System.Text;

namespace SamuraiApp.Domain
{
    class Battle
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
