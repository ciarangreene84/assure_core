using System;
using System.Collections.Generic;
using System.Text;

namespace Assure.Core.RepositoryLayer.Interfaces.Models
{
    public class Currency
    {
        public string Alpha3 { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"{Alpha3}; {Name}";
        }
    }
}
