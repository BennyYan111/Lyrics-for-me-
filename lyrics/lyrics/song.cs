using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lyrics
{
    class song
    {
        public song(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
        public override string ToString()
        {
            return this.Name;
        }
    }
}
