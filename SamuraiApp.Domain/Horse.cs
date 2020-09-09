using System;
using System.Collections.Generic;
using System.Text;

namespace SamuraiApp.Domain
{
    public class Horse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        // SamuraiId = Owner of the Horse
        public int SamuraiId { get; set; }
    }
}
